using System;
using System.Windows.Forms;

namespace SecureBank.Forms
{
    public partial class frmCreateAccount : Form
    {
        public frmCreateAccount()
        {
            InitializeComponent();

        }

        private void btnCreateAccount_Click_1(object sender, EventArgs e)
        {
            frmAddAccount frm = new frmAddAccount();
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }
    }
}
