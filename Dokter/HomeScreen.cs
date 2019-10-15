#region Imports
using Newtonsoft.Json.Linq;
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
        #region Variables
        private Controller controller;
        private delegate void SetData();
        private SetData setData;
        private List<JObject> dataList;
        #endregion

        public HomeScreen()
        {
            InitializeComponent();
            controller = new Controller(this);
            setData += UpdateList;
            dataList = new List<JObject>();
            controller.Connect();
            controller.DataRequest();
        }

        private void UpdateList()
        {
            Console.WriteLine(dataList.Count);

            foreach(JObject jObject in dataList)
            {
                listBox1.Items.Add(($"{jObject["firstName"]} {jObject["lastName"]} {jObject["date"]}"));
            }
        }

        private void HomeScreen_Load(object sender, EventArgs e)
        {
            //Setting max value on the Y axis.
            chartData.ChartAreas["ChartArea1"].AxisY.Maximum = 200;
            chartData.ChartAreas["ChartArea1"].AxisY.Minimum = 0;

            //Setting max value on the X axis.
            chartData.ChartAreas["ChartArea1"].AxisX.Maximum = 1000;
            chartData.ChartAreas["ChartArea1"].AxisX.Minimum = 0;

            //Remove lines on the X axis.
            chartData.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            chartData.ChartAreas["ChartArea1"].AxisX.MinorGrid.Enabled = false;

            //Remove lines on the X axis.
            chartData.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
            chartData.ChartAreas["ChartArea1"].AxisY.MinorGrid.Enabled = false;

        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Debug code to check which index has been selected.
            //MessageBox.Show("Selected item: " + listBox1.SelectedIndex);
        }

        public void AddListBoxItem(JObject obj)
        {
            dataList.Add(obj);
            Invoke(setData);
        }
    }
}