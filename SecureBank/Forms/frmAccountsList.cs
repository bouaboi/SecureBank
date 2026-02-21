using SecureBank.Business;
using SecureBank.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecureBank.Forms
{
    public partial class frmAccountsList : Form
    {
        private DataTable _dgvAccountsList;
        public frmAccountsList()
        {
            InitializeComponent();
        }

        private void frmAccountsList_Load(object sender, EventArgs e)
        {
            LoadAccounts();
        }

        private void LoadAccounts()
        {

            _dgvAccountsList = clsAccount.GetAllAccounts();
            dgvAccounts.DataSource = _dgvAccountsList;
            ConfigureGrid();
            UpdateRecordCount();

            cbFilterby.SelectedIndex = 0;


        }

        private void ConfigureGrid()
        {
            if (dgvAccounts.Rows.Count == 0)
                return;

            dgvAccounts.Columns[0].HeaderText = "Account ID";
            dgvAccounts.Columns[0].Width = 85;

            dgvAccounts.Columns[1].HeaderText = "Account Number";
            dgvAccounts.Columns[1].Width = 100;

            dgvAccounts.Columns[2].HeaderText = "Balance";
            dgvAccounts.Columns[2].Width = 100;

            dgvAccounts.Columns[3].HeaderText = "Is Active";
            dgvAccounts.Columns[3].Width = 55;

            dgvAccounts.Columns[4].HeaderText = "Client ID";
            dgvAccounts.Columns[4].Width = 75;

            dgvAccounts.Columns[5].HeaderText = "First Name";
            dgvAccounts.Columns[5].Width = 100;

            dgvAccounts.Columns[6].HeaderText = "Last Name";
            dgvAccounts.Columns[6].Width = 100;


        }

        private void UpdateRecordCount()
        {
            lblAccountsRecords.Text = _dgvAccountsList.DefaultView.Count.ToString();    
        }

        private void ApplyFilter()
        {

            if (cbFilterby.SelectedIndex == 0)
            {
                _dgvAccountsList.DefaultView.RowFilter = "";
                UpdateRecordCount();
                return;
            }

            if (cbFilterby.Text != "Is Active" && string.IsNullOrWhiteSpace(txtBoxInput.Text))
            {
                _dgvAccountsList.DefaultView.RowFilter = "";
                UpdateRecordCount();
                return;
            }

            string value = txtBoxInput.Text.Replace("'", "' '");
            string filter = "";

            switch(cbFilterby.Text)
            {
                case "Account ID":
                   if (int.TryParse(value, out _))
                        filter = $"AccountId = {value}";
                   break;

                case "Account Number":
                    filter = $"AccountNumber Like '%{value}%'";
                    break;

                case "Client ID":
                    if (int.TryParse(value, out _))
                        filter = $"ClientId = {value}";
                    break;

                case "First Name":
                    filter = $"FirstName Like '%{value}%'";
                    break;

                case "Last Name":
                    filter = $"LastName Like '%{value}%'";
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

            _dgvAccountsList.DefaultView.RowFilter = filter;
            UpdateRecordCount();

        }

        private void txtBoxInput_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void cbFilterby_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtBoxInput.Visible = cbFilterby.Text != "Is Active" && cbFilterby.SelectedIndex != 0;

            rbYes.Visible = rbNo.Visible = cbFilterby.Text == "Is Active";

            ApplyFilter();
        }

        private void rbYes_CheckedChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void rbNo_CheckedChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void txtBoxInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterby.Text == "Client ID" || cbFilterby.Text == "Account ID")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void dgvAccounts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0)
                return;

            int clientId = Convert.ToInt32(dgvAccounts.Rows[e.RowIndex].Cells["ClientID"].Value);

            frmShowEditAccount frm = new frmShowEditAccount(clientId);
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showAccountInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.CurrentCell != null)
            {
                int rowIndex = dgvAccounts.CurrentCell.RowIndex;
                int clientId = Convert.ToInt32(dgvAccounts.Rows[rowIndex].Cells["ClientID"].Value);

                frmShowEditAccount frm = new frmShowEditAccount(clientId);
                frm.ShowDialog();
            }
        }

        private void deleteAccountInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int rowIndex = dgvAccounts.CurrentCell.RowIndex;
            int accountId = Convert.ToInt32(dgvAccounts.Rows[rowIndex].Cells["AccountID"].Value);

            decimal balance = Convert.ToDecimal(dgvAccounts.Rows[rowIndex].Cells["Balance"].Value);

            bool isActive = Convert.ToBoolean(dgvAccounts.Rows[rowIndex].Cells["IsActive"].Value);


            if (!isActive)
            {
                MessageBox.Show("Account Is Alredy Deactivated");
                return;
            }

            if (balance > 0)
            {
                MessageBox.Show("You Cannot Deactivate an account with Balance");
                return;
            }

            var result = MessageBox.Show("Are you sure you want to deactivate this Account?",
                                  "Confirm",
                                  MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
                return;


            if (clsAccount.SoftDeleteAccount(accountId))
            {
                MessageBox.Show("Client deactivated successfully");
                LoadAccounts();
            }
            else
            {
                MessageBox.Show("Delete failed");
            }
        }
    }
}
