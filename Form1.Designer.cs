
namespace NC_axis_display_appli
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.start = new System.Windows.Forms.Button();
            this.stop = new System.Windows.Forms.Button();
            this.graph = new System.Windows.Forms.Button();
            this.ipAddressTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.database = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.XAxis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YAxis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZAxis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZLoad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SPLoad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxFiles = new System.Windows.Forms.ComboBox();
            this.tablegraph = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.BackColor = System.Drawing.Color.MediumAquamarine;
            this.start.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.start.Location = new System.Drawing.Point(6, 33);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(137, 61);
            this.start.TabIndex = 0;
            this.start.Text = "START";
            this.start.UseVisualStyleBackColor = false;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // stop
            // 
            this.stop.BackColor = System.Drawing.Color.DarkSalmon;
            this.stop.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stop.Location = new System.Drawing.Point(253, 33);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(135, 61);
            this.stop.TabIndex = 1;
            this.stop.Text = "STOP";
            this.stop.UseVisualStyleBackColor = false;
            // 
            // graph
            // 
            this.graph.BackColor = System.Drawing.Color.SkyBlue;
            this.graph.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.graph.Location = new System.Drawing.Point(245, 225);
            this.graph.Name = "graph";
            this.graph.Size = new System.Drawing.Size(75, 55);
            this.graph.TabIndex = 2;
            this.graph.Text = "GRAPH";
            this.graph.UseVisualStyleBackColor = false;
            // 
            // ipAddressTextBox
            // 
            this.ipAddressTextBox.Location = new System.Drawing.Point(367, 52);
            this.ipAddressTextBox.Name = "ipAddressTextBox";
            this.ipAddressTextBox.Size = new System.Drawing.Size(138, 19);
            this.ipAddressTextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(242, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Enter IP Address";
            // 
            // database
            // 
            this.database.BackColor = System.Drawing.Color.SkyBlue;
            this.database.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.database.Location = new System.Drawing.Point(430, 225);
            this.database.Name = "database";
            this.database.Size = new System.Drawing.Size(75, 55);
            this.database.TabIndex = 5;
            this.database.Text = "DATABASE";
            this.database.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PapayaWhip;
            this.groupBox1.Controls.Add(this.start);
            this.groupBox1.Controls.Add(this.stop);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(177, 105);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox1.Size = new System.Drawing.Size(394, 103);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sampling Process";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Count,
            this.XAxis,
            this.YAxis,
            this.ZAxis,
            this.ZLoad,
            this.SPLoad});
            this.dataGridView1.Location = new System.Drawing.Point(54, 338);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.Size = new System.Drawing.Size(643, 392);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Count
            // 
            this.Count.HeaderText = "Count";
            this.Count.Name = "Count";
            // 
            // XAxis
            // 
            this.XAxis.HeaderText = "XAxis";
            this.XAxis.Name = "XAxis";
            // 
            // YAxis
            // 
            this.YAxis.HeaderText = "YAxis";
            this.YAxis.Name = "YAxis";
            // 
            // ZAxis
            // 
            this.ZAxis.HeaderText = "ZAxis";
            this.ZAxis.Name = "ZAxis";
            // 
            // ZLoad
            // 
            this.ZLoad.HeaderText = "ZLoad";
            this.ZLoad.Name = "ZLoad";
            // 
            // SPLoad
            // 
            this.SPLoad.HeaderText = "SPLoad";
            this.SPLoad.Name = "SPLoad";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(75, 318);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Sampling Output Data";
            // 
            // comboBoxFiles
            // 
            this.comboBoxFiles.Location = new System.Drawing.Point(806, 60);
            this.comboBoxFiles.Name = "comboBoxFiles";
            this.comboBoxFiles.Size = new System.Drawing.Size(193, 20);
            this.comboBoxFiles.TabIndex = 0;
            // 
            // tablegraph
            // 
            this.tablegraph.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tablegraph.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tablegraph.Location = new System.Drawing.Point(1014, 52);
            this.tablegraph.Name = "tablegraph";
            this.tablegraph.Size = new System.Drawing.Size(83, 32);
            this.tablegraph.TabIndex = 9;
            this.tablegraph.Text = "Table graph";
            this.tablegraph.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.AntiqueWhite;
            this.ClientSize = new System.Drawing.Size(1486, 742);
            this.Controls.Add(this.tablegraph);
            this.Controls.Add(this.comboBoxFiles);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.database);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ipAddressTextBox);
            this.Controls.Add(this.graph);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button stop;
        private System.Windows.Forms.Button graph;
        private System.Windows.Forms.TextBox ipAddressTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button database;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;
        private System.Windows.Forms.DataGridViewTextBoxColumn XAxis;
        private System.Windows.Forms.DataGridViewTextBoxColumn YAxis;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZAxis;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZLoad;
        private System.Windows.Forms.DataGridViewTextBoxColumn SPLoad;
        private System.Windows.Forms.ComboBox comboBoxFiles;
        private System.Windows.Forms.Button tablegraph;
    }
}


