using SecureBank.Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace SecureBank.Forms
{
    public partial class frmUsersList : Form
    {
        public DataTable _dgvUsers;

        public frmUsersList()
        {
            InitializeComponent();
        }

        private void frmUsersList_Load(object sender, EventArgs e)
        {
            LoadUsers();
            cbFilterby.SelectedIndex = 0;

        }

        public void LoadUsers()
        {

            _dgvUsers = clsUsers.GetAllAccounts();
            dgvUsers.DataSource = _dgvUsers;
            ConfigureGrid();
            UpdateRecordCount();


        }

        public void ConfigureGrid()
        {
            if (cbFilterby.SelectedIndex == 0)
                return;

            dgvUsers.Columns[0].HeaderText = "User ID";
            dgvUsers.Columns[0].Width = 50;

            dgvUsers.Columns[1].HeaderText = "User Name";
            dgvUsers.Columns[1].Width = 120;

            dgvUsers.Columns[2].HeaderText = "First Name";
            dgvUsers.Columns[2].Width = 120;

            dgvUsers.Columns[3].HeaderText = "Last Name";
            dgvUsers.Columns[3].Width = 120;



        }

        private void UpdateRecordCount()
        {
            lblUserRecords.Text = _dgvUsers.DefaultView.Count.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void ApplyFilter()
        {



            if (cbFilterby.SelectedIndex == 0)
            {
                _dgvUsers.DefaultView.RowFilter = "";
                UpdateRecordCount();
                return;
            }



            string value = txtBoxInput.Text.Replace("'", "' '");
            string filter = "";

            switch (cbFilterby.Text)
            {
                case "User ID":
                    if (int.TryParse(value, out _))
                        filter = $"UserId = {value}";
                    break;
            }

            _dgvUsers.DefaultView.RowFilter = filter;
            UpdateRecordCount();

        }

        private void txtBoxInput_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void txtBoxInput_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (cbFilterby.Text == "User ID")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void cbFilterby_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBoxInput.Visible = cbFilterby.Text != "None" && cbFilterby.SelectedIndex != 0;
            ApplyFilter();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAddUser frmAddUser = new frmAddUser();
            frmAddUser.ShowDialog();
        }
    }
}
