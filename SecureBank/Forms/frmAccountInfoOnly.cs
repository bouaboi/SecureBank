using SecureBank.Business;
using SecureBank.Global_Classes;
using SecureBank.Models;
using System;
using System.Security.Principal;
using System.Windows.Forms;

namespace SecureBank.Forms
{
    public partial class frmAccountInfoOnly : Form
    {
        private Account _currentAccount;
        private Client _currentClient;
        public frmAccountInfoOnly()
        {
            InitializeComponent();
        }

        private void frmAccountInfoOnly_Load(object sender, EventArgs e)
        {
            _currentClient = clsSession.LoggedInClient;
            _currentAccount = clsAccount.GetAccountByClientId(_currentClient.ClientID);

            Account account = clsAccount.GetAccountByClientId(_currentClient.ClientID);


            if (account == null)
            {
                MessageBox.Show("You do not have account yet, Let's create one");
                frmCreateAccount acc = new frmCreateAccount();
                acc.ShowDialog();
                this.Close();
                return;
            }

            lblAccountD.Text = _currentAccount.AccountId.ToString();
                lblAccNumber.Text = _currentAccount.AccountNumber;
                lblFirstName.Text = _currentAccount.client.FirstName;
                lblLastName.Text = _currentAccount.client.LastName;
                lblBalance.Text = _currentAccount.Balance.ToString();
                txtPinCode.Text = _currentAccount.PinCodeHash;
                lblIsActive.Text = _currentAccount.IsActive ? "Active" : "Not Active";

                label5.Visible = true;
                txtPinCode.Visible = true;
                txtPinCode.Enabled = false;
                txtPinCodeCheck.Visible = false;
                txtPinCodeChecked.Visible = false;
                btnCheck.Visible = false;
                btnDone.Visible = false;
           

                if (_currentAccount.IsActive == false)
                {
                linkLabel1.Visible = false;
                linkLabel2.Visible = false;
                }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.Enabled = false;
            EnabledButtons();
        }


        public void EnabledButtons()
        {
            txtPinCodeCheck.Visible = true;
            txtPinCodeChecked.Visible = true;
            txtPinCodeChecked.Enabled = false;

            btnCheck.Visible = true;
            btnDone.Visible = true;
            btnDone.Enabled = false;

        }
        public Account UpdateClientFromTextBoxes()
        {
            _currentAccount.PinCodeHash = clsUtil.ComputeHash(txtPinCodeChecked.Text.Trim());
            return _currentAccount;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {

            if (clsUtil.ComputeHash(txtPinCodeCheck.Text.Trim()) == _currentAccount.PinCodeHash)
            {
                btnCheck.Enabled = false;
                btnDone.Enabled = true;
                txtPinCodeChecked.Enabled = true;
                txtPinCodeCheck.Enabled = false;
                btnDone.Enabled = true;

            }
            else
            {
                MessageBox.Show("Current Pin Incorrect");
                txtPinCodeChecked.Enabled = false;
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            UpdateClientFromTextBoxes();

            if (clsAccount.UpdateAccount(_currentAccount))
            {
                MessageBox.Show("Account updated successfully.");
                btnDone.Enabled = false;
                txtPinCodeChecked.Enabled = false;
            }
            else
            {
                MessageBox.Show("Update failed.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_currentAccount == null)
            {
                MessageBox.Show("No account to delete.");
                return;
            }

            if (_currentAccount.Balance > 0)
            {
                MessageBox.Show("Please withdraw all funds before closing account.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete your account? This cannot be undone.",
                "Delete Account",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                bool success = clsAccount.SoftDeleteAccount(_currentAccount.AccountId);

                if (success)
                {
                   
                    MessageBox.Show("Account deleted successfully.");
                   
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Delete failed.");
                }
            }
        }
    }
}
