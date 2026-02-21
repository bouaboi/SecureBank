using SecureBank.Forms;
using System;
using System.Windows.Forms;

namespace SecureBank
{
    public partial class frmOpen : Form
    {
        public frmOpen()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            this.Hide();
            frmLogin.ShowDialog();
            this.Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            frmAddClient addClient = new frmAddClient();
            this.Hide();
            addClient.ShowDialog();
            this.Close();
           
        }
    }
}
