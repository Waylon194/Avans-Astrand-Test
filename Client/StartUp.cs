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
            if(CheckCredentials(firstNameTB.Text, lastNameTB.Text, txtAge.Text, txtWeight.Text, btnMale.Checked, btnFemale.Checked))
            {
                if (btnMale.Checked)
                {
                    controller.SetClientData(firstNameTB.Text, lastNameTB.Text, int.Parse(txtAge.Text), int.Parse(txtWeight.Text), "Man");
                }
                else
                {
                    controller.SetClientData(firstNameTB.Text, lastNameTB.Text, int.Parse(txtAge.Text), int.Parse(txtWeight.Text), "Vrouw");
                }

                controller.AstradTestClient = new AstradTestClient(controller);
                controller.AstradTestClient.Show();
                Hide();
            }
        }

        private bool CheckCredentials(string firstName, string lastName, string age, string weight, bool man, bool female)
        {
            int useless;

            if(!IsEmpty(firstName) && !IsEmpty(lastName) && int.TryParse(age, out useless) && int.TryParse(weight, out useless) && (man || female))
            {
                return true;
            }
            return false;
        }

        private bool IsEmpty(string str)
        {
            if (str.Trim() == "")
            {
                return true;
            }
            return false;
        }
    }
}