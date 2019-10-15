#region Imports
using System;
using System.Threading;
using System.Windows.Forms;
#endregion

namespace Client
{
    public partial class StartUp : Form
    {
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
            controller.AstrandTest = new AstrandTest(controller);
            controller.AstrandTest.Show();
            Hide();
        }
    }
}