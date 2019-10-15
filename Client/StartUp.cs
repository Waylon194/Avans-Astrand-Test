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

        private void StartTimer()
        {
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            timer1.Enabled = true;
            timer1.Start();
        }

        private void StartButtonClick(object sender, EventArgs e)
        {
            controller.SetName(firstNameTB.Text, lastNameTB.Text);
            StartTimer();  
            //Load test form

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _ticks++;
            Console.WriteLine("TICK " + _ticks);
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
            controller.runningTest = true;
            Console.WriteLine("WarmUp done!");
        }

        private void astradTest()
        {
            controller.runningTest = false;
            controller.SendTrainingData();
            Console.WriteLine("AstradTest done!");
        }

        private void cooldown()
        {
            Console.WriteLine("WarmUp done!");
        }
    }
}