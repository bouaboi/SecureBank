using System;
using SecureBank.Global_Classes;
using System.Windows.Forms;

namespace SecureBank.Forms
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmClientsList clientsList = new frmClientsList();
            clientsList.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmAccountsList accountsList = new frmAccountsList();
            accountsList.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmTransactions frmTransactions = new frmTransactions();
            frmTransactions.ShowDialog();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            txtCurrentUser.Enabled = false;
            txtUserName.Enabled = false;
            txtUserType.Enabled = false;

            if (clsSession.IsLoggedIn)
            {
                txtCurrentUser.Text = clsSession.FullName;

                if (clsSession.IsEmployee)
                {
                    txtUserType.Text = "Employee";
                    txtUserName.Text = clsSession.LoggedInUser.UserName;
                }
                else if (clsSession.IsClient)
                {
                    txtUserType.Text = "Client";
                    txtUserName.Text = clsSession.LoggedInClient.UserName;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show( "Are you sure you want to logout?", "Logout", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (result == DialogResult.Yes)
            {

                clsSession.Logout();
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmUsersList usersList = new frmUsersList();
            usersList.Show();
        }

    }
}
