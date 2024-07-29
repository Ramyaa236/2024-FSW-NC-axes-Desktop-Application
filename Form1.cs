using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Data.SQLite;
using System.Threading;
using System.Configuration; 


namespace NC_axis_display_appli
{
    public partial class Form1 : Form 
    {
        private bool continueSampling;
        private ushort h;
        private StreamWriter writer;
        private SQLiteConnection sqliteConnection; // DB SQLite



        public Form1()
        {
            InitializeComponent();
            continueSampling = true;
            stop.Click += stop_Click;
            start.Click += start_Click;
            graph.Click += graph_Click;
            tablegraph.Click += tablegraph_Click;
            database.Click += database_Click;

            string lastIPAddress = Properties.Settings.Default.LastIPAddress;
            if (!string.IsNullOrWhiteSpace(lastIPAddress))
            {
                ipAddressTextBox.Text = lastIPAddress;
            }

            //OpenStreamWriter();
            // DB SQLite
            // Initialize SQLite connection C:\Users\2701\Desktop\FOCAS2NC\NCaxisappli\mydata.db, ‪C:\Users\2701\Desktop\FOCAS2NC\NCaxisappli\mydata.db, C:\Users\2701\Desktop\Ramyaa ラムヤ\NC APPLI\modified 2024.01.18 NCaxisappli
            sqliteConnection = new SQLiteConnection("Data Source=U:\\hbsgi13C_AF制御技術部\\501工機標準\\標準不具合、変更\\20230711 FOCAS2活用\\2024.02.7 TXT,SQlite NCaxisappli\\mydata.db");
            sqliteConnection.Open();
            // DB SQLite
            PopulateComboBox();
        }

        private void OpenStreamWriter()
        {
            // Generate a unique file name based on the current timestamp
            string fileName = $"SampledData_{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt";

            // Choose a file path and name for the text file inside your project
            string filePath = Path.Combine(Application.StartupPath, "U:\\hbsgi13C_AF制御技術部\\501工機標準\\標準不具合、変更\\20230711 FOCAS2活用\\2024.02.7 TXT,SQlite NCaxisappli\\NC axis display appli\\Sampled Data Output", fileName);

            // Open StreamWriter in append mode
            writer = new StreamWriter(filePath, true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Code to be executed when the form is loaded
        }

        private async void start_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                StartSampling();
            });
        }

        private int sampleCount = 0; // Variable to keep track of the count

        private void StartSampling()
        {
            short intret;

            Focas1.ODBST buf = new Focas1.ODBST();
            Focas1.IODBPSD param = new Focas1.IODBPSD();
            Focas1.ODBAXIS machine = new Focas1.ODBAXIS();
            Focas1.IODBPMC2 buf2 = new Focas1.IODBPMC2();

            // Get the IP address from the text box
            string ipAddress = ipAddressTextBox.Text;

            // Validate the IP address
            if (string.IsNullOrWhiteSpace(ipAddress))
            {
                MessageBox.Show("Please enter a valid IP address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            //Focas1.ODBST buf = new Focas1.ODBST(); IP address 172.16.100.202   127.0.0.1
            intret = Focas1.cnc_allclibhndl3(ipAddress, 8193, 10, out h);
            if (intret == Focas1.EW_OK)
            {
                double ZPA2086, PA4127, ZLOAD1, SPLOAD1;

                intret = Focas1.cnc_rdparam(h, 2086, 3, 4 + 2 * 1, param);
                ZPA2086 = param.u.idatas[0];
                ZLOAD1 = 100 / ZPA2086;

                intret = Focas1.cnc_rdparam(h, 4127, 1, 4 + 2 * 1, param);
                PA4127 = param.u.idatas[0];
                SPLOAD1 = PA4127 / 32767;

                int SamplingInterval = 0;
                continueSampling = true;


                //string filePath = Path.Combine(Application.StartupPath, "C:\\Users\\2701\\Desktop\\Ramyaa ラムヤ\\NC APPLI\\modified 2024.01.18 NCaxisappli\\NC axis display appli\\Sampled data.txt");

                //// Use a StreamWriter to write the data to the text file
                //using (StreamWriter writer = new StreamWriter(filePath))

                // DB SQLite DB SQLite 

                // Create a New data table and Single common database based on the input from the user
                //string tableName = useNewdataTable ? "Data_" + DateTime.Now.ToString("yyyyMMddHHmmss") : "単一のデータベース";
                //string tableName = "Data_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                string tableNameWithIPAddress = $"IP_{ipAddress.Replace(".", "_")}_{DateTime.Now.ToString("yyyyMMddHHmmss")}";


                //// Create a new table for this run if it doesn't exist
                //using (SQLiteCommand createTableCmd = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS {tableName} (XAxis, YAxis, ZAxis, ZLoad, SPLoad)", sqliteConnection))
                using (SQLiteCommand createTableCmd = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS {tableNameWithIPAddress} (XAxis, YAxis, ZAxis, ZLoad, SPLoad)", sqliteConnection))
                {
                    createTableCmd.ExecuteNonQuery();
                }

                // DB SQLite DB SQLite

                while (continueSampling)
                {
                    intret = Focas1.cnc_machine(h, 1, 4 + 4 * 1, machine);
                    int B1 = machine.data[0];

                    intret = Focas1.cnc_machine(h, 2, 4 + 4 * 1, machine);
                    int B2 = machine.data[0];

                    intret = Focas1.cnc_machine(h, 3, 4 + 4 * 1, machine);
                    int B3 = machine.data[0];

                    intret = Focas1.pmc_rdpmcrng(h, 9, 1, 2954, 2955, 8 + 1 * 2, buf2);
                    double B4 = buf2.ldata[0] * ZLOAD1;

                    intret = Focas1.pmc_rdpmcrng(h, 9, 1, 2936, 2937, 8 + 1 * 2, buf2);
                    double B5 = buf2.ldata[1] * SPLOAD1;

                    // Check if all axis values are zero, skip the line if true
                    if (B1 == 0 && B2 == 0 && B3 == 0)
                    {
                        // Optional: You can log or handle the case where all values are zero
                        continue;
                    }

                    ////Display results in your C# application (e.g., store in a data structure or print to console)
                    Console.WriteLine($"X Axis: {B1}, Y Axis: {B2}, Z Axis: {B3}, Z Load: {B4}, SP Load: {B5}, ZPA2086: {ZPA2086}, ZLOAD1: {ZLOAD1}, PA4127: {PA4127}, SPLOAD1: {SPLOAD1}");


                    //// Append the data to the text file
                    //string outputLine = $"X Axis: {B1}, Y Axis: {B2}, Z Axis: {B3}, Z Load: {B4}, SP Load: {B5}, ZPA2086: {ZPA2086}, ZLOAD1: {ZLOAD1}, PA4127: {PA4127}, SPLOAD1: {SPLOAD1}";

                    //Invoke(new Action(() =>
                    //{
                    //    Console.WriteLine(outputLine);
                    //}));

                    //// Write the data to the text file
                    //writer.WriteLine(outputLine);

                    //// Flush the buffer to ensure the data is written immediately
                    //writer.Flush();

                    //// Optional: If you want to see the changes immediately in the UI, you can use Application.DoEvents()
                    //Application.DoEvents();

                    //if (!continueSampling)
                    //    break;

                    // DB SQLite DB SQLite 
                    using (SQLiteCommand insertCmd = new SQLiteCommand($"INSERT INTO {tableNameWithIPAddress} (XAxis, YAxis, ZAxis, ZLoad, SPLoad) VALUES (@XAxis, @YAxis, @ZAxis, @ZLoad, @SPLoad)", sqliteConnection))
                    {
                        insertCmd.Parameters.AddWithValue("@XAxis", B1);
                        insertCmd.Parameters.AddWithValue("@YAxis", B2);
                        insertCmd.Parameters.AddWithValue("@ZAxis", B3);
                        insertCmd.Parameters.AddWithValue("@ZLoad", B4);
                        insertCmd.Parameters.AddWithValue("@SPLoad", B5);
                        insertCmd.ExecuteNonQuery(); // Execute the INSERT statement
                    }
                    // DB SQLite DB SQLite 
                    sampleCount++;
                    dataGridView1.Invoke((MethodInvoker)delegate {
                        dataGridView1.Rows.Add(sampleCount, B1, B2, B3, B4, B5);
                    });

                    // Sleep for the specified interval
                    Thread.Sleep(SamplingInterval);
                }
                //// Close the StreamWriter when sampling is done
                //writer.Close();
            }
            else
            {
                Console.WriteLine("ERROR!({0})", intret);
            }
        }


        private void stop_Click(object sender, EventArgs e)
        {
            continueSampling = false;
            SaveDataToDatabase();

        }


        private void SaveDataToDatabase()
        {
            try
            {
                // Check if the connection is open, if not, open it
                if (sqliteConnection.State != ConnectionState.Open)
                {
                    sqliteConnection.Open();
                }

                using (SQLiteTransaction transaction = sqliteConnection.BeginTransaction())
                {
                    try
                    {
                        // Perform database operations here

                        // Commit the transaction
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception, and optionally rollback the transaction
                        transaction.Rollback();
                        MessageBox.Show($"Error committing transaction: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // No need to close the connection here, it can remain open
                MessageBox.Show("Data saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data to database: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void graph_Click(object sender, EventArgs e)
        {
            string exePath = @"C:\Users\2701\AppData\Local\miniconda3\python.exe"; // Path to Python executable
            string scriptPath = @"U:\hbsgi13C_AF制御技術部\501工機標準\標準不具合、変更\20230711 FOCAS2活用\2024.02.7 TXT,SQlite NCaxisappli\FOCAS2NCplot\Gradationmap.py"; // Path to Python script

            ProcessStartInfo psi = new ProcessStartInfo(exePath);
            psi.UseShellExecute = false;
            psi.Arguments = $"\"{scriptPath}\"";

            using (Process process = new Process())
            {
                process.StartInfo = psi;
                process.Start();
                process.WaitForExit(); // Wait for the process to exit
            }
        }

        private void database_Click(object sender, EventArgs e)
        {
            try
            {
                // Path to your .db file
                string dbFilePath = @"U:\hbsgi13C_AF制御技術部\501工機標準\標準不具合、変更\20230711 FOCAS2活用\2024.02.7 TXT,SQlite NCaxisappli\mydata.db";

                // Check if the .db file exists
                if (System.IO.File.Exists(dbFilePath))
                {
                    // Path to the DB Browser for SQLite executable
                    string dbBrowserPath = @"C:\Program Files\DB Browser for SQLite\DB Browser for SQLite.exe";

                    // Check if the DB Browser executable exists
                    if (System.IO.File.Exists(dbBrowserPath))
                    {
                        // Start the DB Browser process with the .db file as an argument
                        Process.Start(dbBrowserPath, $"\"{dbFilePath}\"");
                    }
                    else
                    {
                        MessageBox.Show("DB Browser for SQLite is not installed at the specified path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(".db file not found at the specified path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening DB Browser for SQLite: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void PopulateComboBox()
        {
            try
            {
                // Database file path
                string dbFilePath = @"U:\hbsgi13C_AF制御技術部\501工機標準\標準不具合、変更\20230711 FOCAS2活用\2024.02.7 TXT,SQlite NCaxisappli\mydata.db";

                // Connection string
                string connectionString = $"Data Source={dbFilePath};Version=3;";

                // Open connection to the database
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Get table names from the database
                    DataTable tableNames = connection.GetSchema("Tables");

                    // Add table names to the combo box
                    foreach (DataRow row in tableNames.Rows)
                    {
                        string tableName = row["TABLE_NAME"].ToString();
                        comboBoxFiles.Items.Add(tableName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error populating combo box: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tablegraph_Click(object sender, EventArgs e)
        {
                // Get the selected file name from the combo box
                string selectedFileName = comboBoxFiles.SelectedItem.ToString();

                // Path to Python executable
                string exePath = @"C:\Users\2701\AppData\Local\miniconda3\python.exe";

                // Path to Python script
                string scriptPath = @"U:\hbsgi13C_AF制御技術部\501工機標準\標準不具合、変更\20230711 FOCAS2活用\2024.02.7 TXT,SQlite NCaxisappli\FOCAS2NCplot\GradationmapTableGraph.py";

                // Arguments for Python script including the selected file name
                string arguments = $"\"{scriptPath}\" \"{selectedFileName}\"";

                ProcessStartInfo psi = new ProcessStartInfo(exePath);
                psi.UseShellExecute = false;
                psi.Arguments = arguments;

                using (Process process = new Process())
                {
                    process.StartInfo = psi;
                    process.Start();
                    process.WaitForExit();
                }
        }


        //ERROR : System.InvalidOperationException: 'Database is not open'
        //private void SaveDataToDatabase()
        //{
        //    try
        //    {
        //        if (sqliteConnection.State != ConnectionState.Open)
        //        {
        //            MessageBox.Show("Database connection is not open.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }

        //        using (SQLiteTransaction transaction = sqliteConnection.BeginTransaction())
        //        {
        //            try
        //            {
        //                // Commit the transaction
        //                transaction.Commit();
        //            }
        //            catch (Exception ex)
        //            {
        //                // Handle the exception, and optionally rollback the transaction
        //                transaction.Rollback();
        //                MessageBox.Show($"Error committing transaction: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //        }

        //        sqliteConnection.Close();
        //        MessageBox.Show("Data saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error saving data to database: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}


        //private void database_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // Path to your .db file
        //        string dbFilePath = @"C:\\Users\\2701\\Desktop\\Ramyaa ラムヤ\\NC APPLI\\2024.02.7 TXT,SQlite NCaxisappli\\mydata.db";

        //        // Check if the .db file exists
        //        if (System.IO.File.Exists(dbFilePath))
        //        {
        //            // Start the default application associated with .db files
        //            Process.Start(dbFilePath);
        //        }
        //        //// Path to the DB Browser for SQLite executable
        //        //string dbBrowserPath = @"C:\Program Files\DB Browser for SQLite\DB Browser for SQLite.exe";

        //        //// Check if the DB Browser executable exists
        //        //if (System.IO.File.Exists(dbBrowserPath))
        //        //{
        //        //    // Start the DB Browser process
        //        //    Process.Start(dbBrowserPath);
        //        //}
        //        else
        //        {
        //            MessageBox.Show("DB Browser for SQLite is not installed at the specified path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error opening DB Browser for SQLite: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}



        //private void graph_Click(object sender, EventArgs e)

        //{
        //    string exePath = @"C:\Users\2701\AppData\Local\miniconda3\python.exe";//python.exe exepath *check the terminal window in the VS code*
        //    ProcessStartInfo psi = new ProcessStartInfo(exePath);
        //    psi.UseShellExecute = false;
        //    psi.RedirectStandardOutput = true;
        //    psi.RedirectStandardError = true;
        //    //psi.Arguments = @"""U:\hbsgi13C_AF制御技術部\501工機標準\標準不具合、変更\20230711 FOCAS2活用\ラムヤ\FOCAS2NCplot\Gradationmap.py""";     // real location
        //    psi.Arguments = @"""C:\Users\2701\Desktop\Ramyaa ラムヤ\NC APPLI\2024.02.7 TXT,SQlite NCaxisappli\FOCAS2NCplot\Gradationmap.py""";
        //    //"U:\hbsgi13C_AF制御技術部\501工機標準\標準不具合、変更\20230711 FOCAS2活用\ラムヤ\FOCAS2NCplot\Gradationmap.py"    
        //    //"""c:\Users\2701\Desktop\Python workspace\FOCAS2NC\Gradationmap.py"""
        //    // psi.Arguments = @"""U:\\hbsgi13C_AF制御技術部\\501工機標準\\標準不具合、変更\\20230711 FOCAS2活用\\ラムヤ\\FOCAS2NCplot\\Gradationmap.py""";

        //    using (Process process = new Process())
        //    {
        //        //MessageBox.Show(exePath);
        //        //MessageBox.Show(psi.Arguments);
        //        process.StartInfo = psi;
        //        process.Start();

        //        // プロセスの終了を待つ
        //        process.WaitForExit();

        //    }

        //}  

    }

}

///////////////////////////////////////OLD CODE////////////////////////////////////////////////////////
/////working code - FOCAS2 FSW App
//namespace NC_axis_display_appli
//{
//    public partial class Form1 : Form
//    {
//        //private SQLiteConnection sqliteConnection;
//        private bool continueSampling;

//        public Form1()
//        {
//            InitializeComponent();
//            stop.Click += stop_Click;
//            start.Click += start_Click;
//            //// Initialize SQLite connection C:\Users\2701\Desktop\FOCAS2NC\NCaxisappli\mydata.db, ‪C:\Users\2701\Desktop\FOCAS2NC\NCaxisappli\mydata.db
//            //sqliteConnection = new SQLiteConnection("Data Source=U:\\hbsgi13C_AF制御技術部\\501工機標準\\標準不具合、変更\\20230711 FOCAS2活用\\ラムヤ\\NCaxisappli\\mydata.db");
//            //sqliteConnection.Open();
//        }



//        private void start_Click(object sender, EventArgs e)
//        {
//            ushort h;
//            short intret;

//            Focas1.ODBST buf = new Focas1.ODBST();
//            Focas1.IODBPSD param = new Focas1.IODBPSD();
//            Focas1.ODBAXIS machine = new Focas1.ODBAXIS();
//            Focas1.IODBPMC2 buf2 = new Focas1.IODBPMC2();

//            //Focas1.ODBST buf = new Focas1.ODBST(); IP address 172.16.100.202   127.0.0.1
//            intret = Focas1.cnc_allclibhndl3("172.16.100.202", 8193, 10, out h);
//            if (intret == Focas1.EW_OK)
//            {
//                double ZPA2086, PA4127, ZLOAD1, SPLOAD1;

//                intret = Focas1.cnc_rdparam(h, 2086, 3, 4 + 2 * 1, param);
//                ZPA2086 = param.u.idatas[0];
//                ZLOAD1 = 100 / ZPA2086;

//                intret = Focas1.cnc_rdparam(h, 4127, 1, 4 + 2 * 1, param);
//                PA4127 = param.u.idatas[0];
//                SPLOAD1 = PA4127 / 32767;

//                //int i = 1;
//                int SamplingInterval = 50; // Set your desired sampling interval in milliseconds
//                continueSampling = true;

//                //// SQLite DB SQLite DB SQLite DB SQLite DB SQLite DB SQLite DB SQLite DB SQLite DB SQLite DB SQLite DB SQLite DB
//                //// Create a unique table name based on the current timestamp
//                //string tableName = "Data_" + DateTime.Now.ToString("yyyyMMddHHmmss");

//                //// Create a new table for this run if it doesn't exist
//                //using (SQLiteCommand createTableCmd = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS {tableName} (XAxis, YAxis, ZAxis, ZLoad, SPLoad)", sqliteConnection))
//                //{
//                //    createTableCmd.ExecuteNonQuery();
//                //}
//                // SQLite DB SQLite DB SQLite DB SQLite DB SQLite DB SQLite DB SQLite DB SQLite DB SQLite DB SQLite DB SQLite DB

//                while (continueSampling)
//                {
//                    intret = Focas1.cnc_machine(h, 1, 4 + 4 * 1, machine);
//                    int B1 = machine.data[0];

//                    intret = Focas1.cnc_machine(h, 2, 4 + 4 * 1, machine);
//                    int B2 = machine.data[0];

//                    intret = Focas1.cnc_machine(h, 3, 4 + 4 * 1, machine);
//                    int B3 = machine.data[0];

//                    intret = Focas1.pmc_rdpmcrng(h, 9, 1, 2954, 2955, 8 + 1 * 2, buf2);
//                    double B4 = buf2.ldata[0] * ZLOAD1;

//                    intret = Focas1.pmc_rdpmcrng(h, 9, 1, 2936, 2937, 8 + 1 * 2, buf2);
//                    double B5 = buf2.ldata[1] * SPLOAD1;

//                    // Display results in your C# application (e.g., store in a data structure or print to console)
//                    Console.WriteLine($"X Axis: {B1}, Y Axis: {B2}, Z Axis: {B3}, Z Load: {B4}, SP Load: {B5}, ZPA2086: {ZPA2086}, ZLOAD1: {ZLOAD1}, PA4127: {PA4127}, SPLOAD1: {SPLOAD1}");

//                    //// SQLite DB SQLite DB SQLite DB SQLite DB SQLite DB SQLite DB SQLite DB SQLite DB SQLite DB SQLite DB SQLite DB
//                    //using (SQLiteCommand insertCmd = new SQLiteCommand($"INSERT INTO {tableName} (XAxis, YAxis, ZAxis, ZLoad, SPLoad) VALUES (@XAxis, @YAxis, @ZAxis, @ZLoad, @SPLoad)", sqliteConnection))
//                    //{
//                    //    insertCmd.Parameters.AddWithValue("@XAxis", B1);
//                    //    insertCmd.Parameters.AddWithValue("@YAxis", B2);
//                    //    insertCmd.Parameters.AddWithValue("@ZAxis", B3);
//                    //    insertCmd.Parameters.AddWithValue("@ZLoad", B4);
//                    //    insertCmd.Parameters.AddWithValue("@SPLoad", B5);
//                    //    insertCmd.ExecuteNonQuery(); // Execute the INSERT statement
//                    //}
//                    // SQLite DB SQLite DB SQLite DB SQLite DB SQLite DB SQLite DB SQLite DB SQLite DB SQLite DB SQLite DB SQLite DB

//                    //i++;

//                    // Implement your stop condition (e.g., based on a button click)

//                    // Simulate a pause for the specified SamplingInterval
//                    System.Threading.Thread.Sleep(SamplingInterval);
//                }
//            }
//            else
//            {
//                Console.WriteLine("ERROR!({0})", intret);
//            }

//            //// The path to your Python script
//            //string pythonScriptPath = "C:\\Users\\2701\\Desktop\\Python workspace\\FOCAS2\\heatmap.py";

//            //// Additional arguments for the Python script (if needed)
//            ////string pythonArguments = "";

//            //// Create a process to run the Python script
//            //Process pythonProcess = new Process();
//            //ProcessStartInfo pythonStartInfo = new ProcessStartInfo
//            //{
//            //    FileName = "C:\\Users\\2701\\Desktop\\Python workspace\\FOCAS2\\heatmap.py", // Path to the Python interpreter, or just "python" if it's in your system's PATH.
//            //    //Arguments = $"\"{pythonScriptPath}\" {pythonArguments}",
//            //    RedirectStandardOutput = true,
//            //    UseShellExecute = false,
//            //    CreateNoWindow = true
//            //};

//            //pythonProcess.StartInfo = pythonStartInfo;
//            //pythonProcess.Start();

//            //string pythonOutput = pythonProcess.StandardOutput.ReadToEnd();
//            //pythonProcess.WaitForExit();

//            //// Display the Python output in your C# application
//            //Console.WriteLine("Python output: " + pythonOutput);

//            //         DisplayDataInDBBrowser();
//        }

//        private void stop_Click(object sender, EventArgs e)
//        {
//            continueSampling = false;
//        }


//        private void Form1_Load(object sender, EventArgs e)
//        {

//        }

//        //protected override void OnFormClosing(FormClosingEventArgs e)
//        //{
//        //    base.OnFormClosing(e);
//        //    // Close the SQLite connection when the form is closed
//        //    sqliteConnection.Close();
//        //}
//    }
//}
