using SecureBank.Business;
using SecureBank.Models;
using System;
using System.Data;
using System.Windows.Forms;

namespace SecureBank.Forms
{
    public partial class frmClientsList : Form
    {
        private DataTable _dgvClientsList;
        public frmClientsList()
        {
            InitializeComponent();
        }

        private void frmClient_Load(object sender, EventArgs e)
        {
            LoadClients();  
        }

        private void LoadClients()
        {

            _dgvClientsList = clsClients.GetAllClients();
            dgvClients.DataSource = _dgvClientsList;

            ConfigureGrid();
            UpdateRecordsCount();


            cbFilterby.SelectedIndex = 0;
        }

        private void ConfigureGrid()
        {
            if (dgvClients.Rows.Count == 0)
                return;

            dgvClients.Columns[0].HeaderText = "Client ID";
            dgvClients.Columns[0].Width = 85;

            dgvClients.Columns[1].HeaderText = "First Name";
            dgvClients.Columns[1].Width = 100;

            dgvClients.Columns[2].HeaderText = "Last Name";
            dgvClients.Columns[2].Width = 100;

            dgvClients.Columns[3].HeaderText = "Email";
            dgvClients.Columns[3].Width = 140;

            dgvClients.Columns[4].HeaderText = "Phone";
            dgvClients.Columns[4].Width = 100;

            dgvClients.Columns[5].HeaderText = "Address";
            dgvClients.Columns[5].Width = 100;

            dgvClients.Columns[6].HeaderText = "Is Active";
            dgvClients.Columns[6].Width = 55;
        }

        private void cbFilterby_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtBoxInput.Visible = cbFilterby.Text != "Is Active" && cbFilterby.SelectedIndex != 0;

            rbYes.Visible = rbNo.Visible = cbFilterby.Text == "Is Active";

            ApplyFilter();
        }

        private void txtBoxInput_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void ApplyFilter()
        {

            if (cbFilterby.SelectedIndex == 0)
            {
                _dgvClientsList.DefaultView.RowFilter = "";
                UpdateRecordsCount();
                return;
            }

            if (cbFilterby.Text != "Is Active" &&
                string.IsNullOrWhiteSpace(txtBoxInput.Text))
            {
                _dgvClientsList.DefaultView.RowFilter = "";
                UpdateRecordsCount();
                return;
            }

            string value = txtBoxInput.Text.Replace("'", "''");
            string filter = "";

            switch (cbFilterby.Text)
            {
                case "Client ID":
                    if (int.TryParse(value, out _))
                        filter = $"ClientID = {value}";
                    break;

                case "First Name":
                    filter = $"FirstName LIKE '%{value}%'";
                    break;

                case "Last Name":
                    filter = $"LastName LIKE '%{value}%'";
                    break;

                case "Email":
                    filter = $"Email LIKE '%{value}%'";
                    break;

                case "Phone":
                    filter = $"Phone LIKE '%{value}%'";
                    break;

                case "Address":
                    filter = $"Address LIKE '%{value}%'";
                    break;

                case "Is Active":

                    if (rbYes.Checked)
                        filter = "IsActive = true";

                    else if (rbNo.Checked)
                        filter = "IsActive = false";
                    else
                        filter = "";
                    break;
            }

            _dgvClientsList.DefaultView.RowFilter = filter;
            UpdateRecordsCount();
        }

        private void UpdateRecordsCount()
        {
            lblClientsRecords.Text = _dgvClientsList.DefaultView.Count.ToString();
        }

        private void rbNo_CheckedChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void rbYes_CheckedChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void txtValidate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterby.Text == "Client ID" || cbFilterby.Text == "Phone")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void dgvClients_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            int clientId = Convert.ToInt32(dgvClients.Rows[e.RowIndex].Cells["ClientID"].Value);

            frmShowAndEdit frm = new frmShowAndEdit(clientId);
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showClientInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvClients.CurrentCell != null)
            {
                int rowIndex = dgvClients.CurrentCell.RowIndex;
                int clientId = Convert.ToInt32(dgvClients.Rows[rowIndex].Cells["ClientID"].Value);

                frmShowAndEdit frm = new frmShowAndEdit(clientId);
                frm.ShowDialog();
            }
        }

        private void deleteClientInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int rowIndex = dgvClients.CurrentCell.RowIndex;
            int clientId = Convert.ToInt32(dgvClients.Rows[rowIndex].Cells["ClientID"].Value);


            bool isActive = Convert.ToBoolean(dgvClients.Rows[rowIndex].Cells["IsActive"].Value);


            if (!isActive)
            {
                MessageBox.Show("Client Is Alredy Deactivated");
                return;
            }

            var result = MessageBox.Show("Are you sure you want to deactivate this client?",
                                  "Confirm",
                                  MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
                return;


            if (clsClients.SoftDeleteClient(clientId))
            {
                MessageBox.Show("Client deactivated successfully");
                LoadClients();
            }
            else
            {
                MessageBox.Show("Delete failed");
            }

        }

        private void activateClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowIndex = dgvClients.CurrentCell.RowIndex;
            int clientId = Convert.ToInt32(dgvClients.Rows[rowIndex].Cells["ClientID"].Value);

            bool isActive = Convert.ToBoolean(dgvClients.Rows[rowIndex].Cells["IsActive"].Value);

            if (isActive)
            {
                MessageBox.Show("Client is already active");
                return;
            }

            if (clsClients.ActivateClient(clientId))
            {
                MessageBox.Show("Client activated successfully");
                LoadClients();
            }
            else
            {
                MessageBox.Show("Activate failed");
            }
        }
    }
}
