#region Imports
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace Dokter
{
    public partial class HomeScreen : Form
    {
        private Controller controller;

        public HomeScreen()
        {
            InitializeComponent();
            controller = new Controller(this);
            //controller.Connect();
            //controller.DataRequest();
        }

        private void HomeScreen_Load(object sender, EventArgs e)
        {
            //Setting max value on the Y axis
            chartData.ChartAreas["ChartArea1"].AxisY.Maximum = 200;
            chartData.ChartAreas["ChartArea1"].AxisY.Minimum = 0;

            //Setting max value on the X axis
            chartData.ChartAreas["ChartArea1"].AxisX.Maximum = 1000;
            chartData.ChartAreas["ChartArea1"].AxisX.Minimum = 0;

            //Remove lines on the X axis
            chartData.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            chartData.ChartAreas["ChartArea1"].AxisX.MinorGrid.Enabled = false;

            //Remove lines on the X axis
            chartData.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
            chartData.ChartAreas["ChartArea1"].AxisY.MinorGrid.Enabled = false;

            //Adding some stuff to the listbox
            for (int i = 0; i <= 25; i++)
            {
                listBox1.Items.Add("Waylon Lodder - " + DateTime.Now.AddHours(i));
            }
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Selected item: " + listBox1.SelectedIndex);
        }
    }
}