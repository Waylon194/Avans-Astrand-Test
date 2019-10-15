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
            controller.Age = int.Parse(txtAge.Text);
            Thread controllerThread = new Thread(controller.Start);
            controllerThread.Start();
        }
        private void StartButtonClick(object sender, EventArgs e)
        {
            controller.SetName(firstNameTB.Text, lastNameTB.Text);
            controller.AstradTestClient = new AstradTestClient(controller);
            controller.AstradTestClient.Show();
            Hide();
        }
    }
}