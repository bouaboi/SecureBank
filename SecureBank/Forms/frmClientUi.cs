using SecureBank.Global_Classes;
using System;
using System.Windows.Forms;

namespace SecureBank.Forms
{
    public partial class frmClientUi : Form
    {
        public frmClientUi()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmClientInfoOnly infoOnly = new frmClientInfoOnly();
            infoOnly.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmAccountInfoOnly accountInfoOnly = new frmAccountInfoOnly();
            accountInfoOnly.ShowDialog();
        }
 
        private void button3_Click(object sender, EventArgs e)
        {
            frmOperationsMyOnly operationsMyOnly = new frmOperationsMyOnly();
            operationsMyOnly.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                clsSession.Logout();
               frmLogin login = new frmLogin();
                this.Hide();
                login.ShowDialog();
            }
        }
    }
}
