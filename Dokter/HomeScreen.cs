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
using System.Windows.Forms.DataVisualization.Charting;
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
            listBox1.Items.Clear();

            foreach(JObject jObject in dataList)
            {
                listBox1.Items.Add(($"{jObject["firstName"]} {jObject["lastName"]} {jObject["date"]}"));
            }
        }

        //Put the selected data in the chart
        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.chartData.Series.Clear();

            //Setting max value on the Y axis.
            chartData.ChartAreas["ChartArea1"].AxisY.Maximum = 200;
            chartData.ChartAreas["ChartArea1"].AxisY.Minimum = 0;

            //Setting max value on the X axis.
            chartData.ChartAreas["ChartArea1"].AxisX.Maximum = 10000;
            chartData.ChartAreas["ChartArea1"].AxisX.Minimum = 0;

            SetChartSettings(this.chartData, "BPM");
            SetChartSettings(this.chartData, "RPM");
            SetChartSettings(this.chartData, "Resistance");

            int itemIndex = listBox1.SelectedIndex;

            JObject selected = dataList[itemIndex];
            JArray data = selected["bikeData"].ToObject<JArray>();

            chartData.ChartAreas["ChartArea1"].AxisX.Maximum = data.Count;

            double maxHeight = chartData.ChartAreas["ChartArea1"].AxisX.Maximum;

            for (int i = 0; i < data.Count; i++)
            {
                JObject timeStamp = data[i].ToObject<JObject>();

                AddPoint(this.chartData, i, "BPM", "bpm", timeStamp, ref maxHeight);
                AddPoint(this.chartData, i, "RPM", "rpm", timeStamp, ref maxHeight);
                AddPoint(this.chartData, i, "Resistance", "resistance", timeStamp, ref maxHeight);
            }
        }

        private void SetChartSettings(Chart chart, string value)
        {
            chart.Series.Add(value);
            chart.Series[value].ChartType = SeriesChartType.Line;
            chart.Series[value].BorderWidth = 3;
        }

        private void AddPoint(Chart chart, int x, string value, string jsonValue, JObject timeStamp, ref double maxHeight)
        {
            int yCoordinate = timeStamp[jsonValue].ToObject<int>();

            if (value.Equals("Resistance"))
            {
                yCoordinate /= 2;
            }

            this.chartData.Series[value].Points.AddXY(x, yCoordinate);
        }

        public void AddListBoxItem(JObject obj)
        {
            dataList.Add(obj);
            Invoke(setData);
        }
    }
}