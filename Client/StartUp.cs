#region Imports
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace Client
{
    public partial class StartUp : Form
    {

        Timer timer1 = new Timer();
        private int _ticks;

        public StartUp()
        {
            InitializeComponent();
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