using SecureBank.Business;
using SecureBank.Global_Classes;
using SecureBank.Models;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SecureBank.Forms
{
    
    public partial class frmAddClient : Form
    {
        
        public frmAddClient()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
                return;

            Client client = new Client();

            client.FirstName = txtFirsName.Text;
            client.LastName = txtLastName.Text;
            client.Email = txtEmail.Text;
            client.Phone = txtPhone.Text;
            client.Address = txtAddress.Text;
            client.UserName = txtUserName.Text;
            client.PasswordHash = clsUtil.ComputeHash(txtPassword.Text);

            int realClientId = clsClients.AddNewClient(client);


            if (realClientId > 0)
            {
                client.ClientID = realClientId;

                clsSession.NewClientId(client);

                MessageBox.Show("Client is Addedd successfully with ID = " + realClientId);


                frmCreateAccount frmCreateAccount = new frmCreateAccount();
                frmCreateAccount.ShowDialog();


                this.DialogResult = DialogResult.OK;
                this.Close();


            }
            else
            {
                MessageBox.Show("Client Add is Failed");
            }
        }

        private void txtValidate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtFirsName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirsName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFirsName, "Please Enter Your First Name");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFirsName, "");
            }
        }

        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLastName, "Please Enter Your Last Name");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtLastName, "");
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Please Enter Your Email");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtEmail, "");
            }
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "Please Enter a User Name");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtUserName, "");
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "Please Enter a Password");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtUserName, "");
            }
        }
    }
}
