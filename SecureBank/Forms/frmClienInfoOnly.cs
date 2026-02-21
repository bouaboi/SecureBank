using SecureBank.Business;
using SecureBank.Global_Classes;
using SecureBank.Models;
using System;
using System.Windows.Forms;

namespace SecureBank.Forms
{
    public partial class frmClientInfoOnly : Form
    {

        private Client _currentClient;

        public frmClientInfoOnly()
        {
            InitializeComponent();

        }

        private void frmClientInfoOnly_Load(object sender, EventArgs e)
        {
            LoadClient();
        }

        public void LoadClient()
        {
            _currentClient = clsSession.LoggedInClient;

            if (_currentClient != null) 
            {
                _currentClient = clsClients.GetClientById(_currentClient.ClientID);
            }

            if (_currentClient == null)
            {
                MessageBox.Show("Client profile not found or has been deleted!");
                clsSession.Logout(); 
         
                frmLogin login = new frmLogin();
                login.Show();

                this.Close();
                return;
            }


            txtFirstName.Visible = false;
            txtLastName.Visible = false;
            txtEmail.Visible = false;
            txtPhone.Visible = false;
            txtAddress.Visible = false;

            btnSave.Visible = false;

            lblClientID.Text = _currentClient.ClientID.ToString();
            lblFirstName.Text = _currentClient.FirstName;
            lblLastName.Text = _currentClient.LastName;
            lblEmail.Text = _currentClient.Email;
            lblPhone.Text = _currentClient.Phone;
            lblAddress.Text = _currentClient.Address;
            lblIsActive.Text = _currentClient.IsActive ? "Active" : "Not Active";


            lblFirstName.Visible = true;
            lblLastName.Visible = true;
            lblEmail.Visible = true;
            lblPhone.Visible = true;
            lblAddress.Visible = true;

        }

        public void EnabledButtons()
        {
            txtFirstName.Visible = true;
            txtLastName.Visible = true;
            txtEmail.Visible = true;
            txtPhone.Visible = true;
            txtAddress.Visible = true;

            lblFirstName.Visible = false;
            lblLastName.Visible = false;
            lblEmail.Visible = false;
            lblPhone.Visible = false;
            lblAddress.Visible = false;
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            btnSave.Visible = true;
            EnabledButtons();
            FillTextBoxesFromClient();
            linkLabel1.Enabled = false;
        }

        private void FillTextBoxesFromClient()
        {
            txtFirstName.Text = _currentClient.FirstName;
            txtLastName.Text = _currentClient.LastName;
            txtEmail.Text = _currentClient.Email;
            txtPhone.Text = _currentClient.Phone;
            txtAddress.Text = _currentClient.Address;
        }
        public Client UpdateClientFromTextBoxes()
        {

            _currentClient.FirstName = txtFirstName.Text.Trim();
            _currentClient.LastName = txtLastName.Text.Trim();
            _currentClient.Email = txtEmail.Text.Trim();
            _currentClient.Phone = txtPhone.Text.Trim();
            _currentClient.Address = txtAddress.Text.Trim();

            return _currentClient;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateClientFromTextBoxes();

            if (clsClients.UpdateClient(_currentClient))
            {
                MessageBox.Show("Client updated successfully.");
                LoadClient();
                btnSave.Visible = false;
            }
            else
            {
                MessageBox.Show("Update failed.");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Account account = clsAccount.GetAccountByClientId(_currentClient.ClientID);

            if (account != null && account.IsActive)  
            {
                MessageBox.Show("Please close your account before deleting profile.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete your profile? This cannot be undone.",
                "Delete Profile",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                bool success = clsClients.SoftDeleteClient(_currentClient.ClientID);

                if (success)
                {
                    MessageBox.Show("Profile deleted successfully. You will now be logged out.");
                    clsSession.Logout();

                    frmLogin login = new frmLogin();
                    login.ShowDialog();

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
