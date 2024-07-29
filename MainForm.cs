using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;



namespace NC_axis_display_appli
{


    public partial class MainForm : Form
    {
        private bool continueSampling = false;
        private IntPtr FlibHndl = IntPtr.Zero;
        private string strIPadd = "";
        private int intPortNo = 0;
        private double ZPA2086 = 0.0;
        private double ZLOAD1 = 0.0;
        private double PA4127 = 0.0;
        private double SPLOAD1 = 0.0;
        private double SamplingInterval = 0.0;
        private int i = 0;
        private double[] B = new double[4]; // Corresponds to Dim B(3) As Variant in VBA


        public MainForm()
        {
            InitializeComponent();
        }



        private void Startbutton_Click(object sender, EventArgs e)
        {
            // Code to start sampling
            continueSampling = true;
            SampleAxes();
        }

        private void Stopbutton_Click(object sender, EventArgs e)
        {
            // Code to stop sampling
            continueSampling = false;
        }

        private void SampleAxes()
        {
            // Your sampling code goes here
            // You can call functions to connect, sample, and display results
            // Make sure to update the DataGridView with the results
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}

