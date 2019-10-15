using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class AstrandTest : Form
    {
        private Controller controller;
        private Timer timer1 = new Timer();
        private delegate void SetData(int rpm, int bpm, int resistance, string message, int remainingTime);
        private SetData setData;


        public AstrandTest(Controller controller)
        {
            InitializeComponent();
            this.controller = controller;
            setData = new SetData(SetDataFunction);
            StartTimer();
        }
        private void StartTimer()
        {
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            timer1.Enabled = true;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            controller.Tick();
        }

        private void SetDataFunction(int rpm, int bpm, int resistance, string message, int remainingTime)
        {
            
        }


        public void DataUpdate(int rpm, int bpm, int resistance, string message, int remainingTime)
        {
            setData.Invoke(rpm, bpm, resistance, message, remainingTime);
        }
    }
}
