using SecureBank.Models;
using SecureBank.Business;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using SecureBank.Global_Classes;


namespace SecureBank.Forms
{
    public partial class frmAddAccount : Form
    {
        private Client _IsNewClient;
        private Client _CurrentClient;
        private Client _targetClient;

        public frmAddAccount()
        {
            InitializeComponent();
  
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
                return;


            if (_targetClient != null)
            {
                if (_targetClient != null)
                {

                    if (_targetClient.ClientID <= 0)
                    {

                        int newId = clsClients.AddNewClient(_targetClient);
                        if (newId <= 0)
                        {
                            MessageBox.Show("Failed to save client in database.");
                            return;
                        }
                        _targetClient.ClientID = newId;

                    }

                    Account account = new Account
                    {
                        client = _targetClient,
                        PinCodeHash = clsUtil.ComputeHash(txtPinCode.Text)
                    };

                    int accountId = clsAccount.AddNewAccount(account);


                    if (accountId > 0)
                    {
                        account.AccountId = accountId;
                        MessageBox.Show("Account added successfully with ID = " + accountId);

                        this.DialogResult = DialogResult.OK;
                        this.Close();

                        frmLogin login = new frmLogin();
                        login.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Account Add Failed");
                    }
                }
            }
            this.Close();
        }



        private void txtPinCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        

        private void txtPinCode_Validating(object sender, CancelEventArgs e)
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

        private void frmAddAccount_Load(object sender, EventArgs e)
        {
            _IsNewClient = clsSession.ForNewClient;
            _CurrentClient = clsSession.LoggedInClient;


            if (_CurrentClient != null)
            {
                _CurrentClient = clsClients.GetClientById(_CurrentClient.ClientID);
            }

            _targetClient = _CurrentClient ?? _IsNewClient;

            if (_targetClient == null)
            {
                MessageBox.Show("No client available to create account.");
                this.Close();
                return;
            }

            txtClientId.Text = _targetClient.ClientID.ToString();
            txtClientId.Enabled = false;

        }

    }
}
