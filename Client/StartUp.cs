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
        Controller controller;

        public StartUp()
        {
            InitializeComponent();
            controller = new Controller();
            Thread controllerThread = new Thread(controller.Start);
            controllerThread.Start();
        }

        private void StartButtonClick(object sender, EventArgs e)
        {
            controller.SetName(firstNameTB.Text, lastNameTB.Text);
            timer1.Start();  
            //Load test form

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
        }

        private void warmup()
        {
            controller.runningTest = false;
        }

        private void astradTest()
        {
            controller.runningTest = true;
            controller.SendTrainingData();
        }

        private void cooldown()
        {
            controller.runningTest = false;
        }
    }
}