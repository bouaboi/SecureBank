using SecureBank.Global_Classes;
using SecureBank.Models;
using SecureBank.Business;
using System;
using System.Windows.Forms;

namespace SecureBank.Forms
{
    public partial class frmLogin : Form
    {
        
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter username and password");
                return;
            }

            string passwordHash = clsUtil.ComputeHash(password);

            if (rbUser.Checked)
            {
                User user = clsLogin.ValidateUser(username, passwordHash);

                if (user != null)
                {
                    clsSession.LoginAsUser(user);

                    frmMain main = new frmMain();
                    this.Hide();
                    main.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid username or password");
                }
            }
            else if (rbClient.Checked)
            {

                Client client = clsLogin.ValidateClient(username, passwordHash);


                if (client != null)
                {
                    Account account = clsAccount.GetAccountByClientId(client.ClientID);

                    clsSession.LoginAsClient(client);


                    if (account != null && account.IsActive)
                    {
                        frmClientPin pin = new frmClientPin();
                        this.Hide();
                        pin.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        frmClientUi ui = new frmClientUi();
                        this.Hide();
                        ui.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid username or password");
                    frmLogin login = new frmLogin();
                    this.Hide();
                    login.ShowDialog();
                    this.Close();

                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmOpen open = new frmOpen();
            this.Hide();
            open.ShowDialog();
            this.Close();
        }

        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowPassword.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }
    }
}
