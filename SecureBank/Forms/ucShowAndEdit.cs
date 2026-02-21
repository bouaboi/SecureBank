using SecureBank.Models;
using System.Windows.Forms;

namespace SecureBank.Forms
{
    public enum ClientFormMode { Show, Edit }

    public partial class ucShowAndEdit : UserControl
    {

        private Client _Client;
        private ClientFormMode _Mode;


        public ucShowAndEdit()
        {
            InitializeComponent();
        }

        public void SwitchToEditMode()
        {
            _Mode = ClientFormMode.Edit;
            lblInfoOrUpdate.Text = "Update Info";


            lblFirstName.Visible = false;
            lblLastName.Visible = false;
            lblEmail.Visible = false;
            lblPhone.Visible = false;
            lblAddress.Visible = false;
            lblIsActive.Visible = false;
            label8.Visible = false;

            txtFirstName.Visible = true;
            txtLastName.Visible = true;
            txtEmail.Visible = true;
            txtPhone.Visible = true;
            txtAddress.Visible = true;

            txtFirstName.Text = _Client.FirstName;
            txtLastName.Text = _Client.LastName;
            txtEmail.Text = _Client.Email;
            txtPhone.Text = _Client.Phone;
            txtAddress.Text = _Client.Address;
        }


        public void LoadClient(Client client, ClientFormMode mode)
        {
            _Client = client;
            _Mode = mode;

            lblClientID.Text = client.ClientID.ToString();
            lblFirstName.Text = client.FirstName;
            lblLastName.Text = client.LastName;
            lblEmail.Text = client.Email;
            lblPhone.Text = client.Phone;
            lblAddress.Text = client.Address;
            lblIsActive.Text = client.IsActive ? "Active" : "Not Active";

            if (mode == ClientFormMode.Show)
            {
                txtFirstName.Visible = false;
                txtLastName.Visible = false;
                txtEmail.Visible = false;
                txtPhone.Visible = false;
                txtAddress.Visible = false;

                lblFirstName.Visible = true;
                lblLastName.Visible = true;
                lblEmail.Visible = true;
                lblPhone.Visible = true;
                lblAddress.Visible = true;
                lblIsActive.Visible = true;
                label8.Visible = true;
            }
            else
            {
                SwitchToEditMode();
            }
        }

        public Client GetUpdatedClient()
        {
            if (_Mode != ClientFormMode.Edit)
                return _Client;

            _Client.FirstName = txtFirstName.Text.Trim();
            _Client.LastName = txtLastName.Text.Trim();
            _Client.Email = txtEmail.Text.Trim();
            _Client.Phone = txtPhone.Text.Trim();
            _Client.Address = txtAddress.Text.Trim();

            return _Client;
        }

    }
}
