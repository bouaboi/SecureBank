using System;
using SecureBank.Models;
using System.Windows.Forms;
using SecureBank.Global_Classes;
using SecureBank.Business;

namespace SecureBank.Forms
{
    public partial class frmClientPin : Form
    {
        private Account _CurrentAccount;
        private Client _CurrentClient;
        public frmClientPin()
        {
            InitializeComponent();
        }

        private void frmClientPin_Load(object sender, EventArgs e)
        {
            _CurrentClient = clsSession.LoggedInClient;
            _CurrentAccount = clsAccount.GetAccountByClientId(_CurrentClient.ClientID);
        }

        public void CheckPinCode()
        {
            if (_CurrentAccount.PinCodeHash != clsUtil.ComputeHash(txtPinCode.Text.Trim()))
            {
                MessageBox.Show("Incorrect PinCode");
                return;
            }
            else
            {
                frmClientUi only = new frmClientUi();
                only.ShowDialog();
                this.Hide();
            }

        }

        private void txtPinCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPinCode_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPinCode.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPinCode, "Please Enter Your PinCode");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtPinCode, "");
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            CheckPinCode();
        }
    }
}
