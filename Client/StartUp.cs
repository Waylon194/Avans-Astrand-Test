#region Imports
using System;
using System.Threading;
using System.Windows.Forms;
#endregion

namespace Client
{
    public partial class StartUp : Form
    {
        System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
        private int _ticks;

        public StartUp()
        {
            InitializeComponent();
            Thread controllerThread = new Thread(Controller.CreateBike);
            controllerThread.Start();
            timer1.Start();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            timer1.Start();  
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _ticks++;

            if (_ticks == 120) //2min warmup
            {
                warmup();
            }

            if (_ticks == 360) //4min test
            {
                astradTest();
            }

            if (_ticks == 420) //1min cooldown
            {
                cooldown();
            }

            if(_ticks > 420) // Send test data to the server
            {

            }
        }

        private void warmup()
        {

        }

        private void astradTest()
        {

        }

        private void cooldown()
        {

        }
    }
}