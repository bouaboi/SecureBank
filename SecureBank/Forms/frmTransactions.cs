using SecureBank.Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace SecureBank.Forms
{
    public partial class frmTransactions : Form
    {

        private DataTable _dgvTransactionList;
        public frmTransactions()
        {
            InitializeComponent();
        }

        private void frmTransactions_Load(object sender, EventArgs e)
        {
            LoadTransaction();
            cbFilterby.SelectedIndex = 0;

        }
        private void LoadTransaction()
        {

            _dgvTransactionList = clsTransactions.GetAllTransactions();
            dgvTransactions.DataSource = _dgvTransactionList;

            ConfigureGrid();
            UpdateRecordCount();
        }

        public void ConfigureGrid()
        {
            if (dgvTransactions.Rows.Count == 0)
                return;


            dgvTransactions.Columns[0].HeaderText = "Transaction ID";
            dgvTransactions.Columns[0].Width = 85;

            dgvTransactions.Columns[1].HeaderText = "Transaction Date";
            dgvTransactions.Columns[1].Width = 100;

            dgvTransactions.Columns[2].HeaderText = "Amount";
            dgvTransactions.Columns[2].Width = 100;

            dgvTransactions.Columns[3].HeaderText = "Description";
            dgvTransactions.Columns[3].Width = 100;

            dgvTransactions.Columns[4].HeaderText = "From AccountID";
            dgvTransactions.Columns[4].Width = 100;

            dgvTransactions.Columns[5].HeaderText = "To AccountID";
            dgvTransactions.Columns[5].Width = 100;

            dgvTransactions.Columns[6].HeaderText = "Type Name";
            dgvTransactions.Columns[6].Width = 85;

            dgvTransactions.Columns[7].HeaderText = "Status Name";
            dgvTransactions.Columns[7].Width = 85;



        }

        private void UpdateRecordCount()
        {
            lblAccountsRecords.Text = _dgvTransactionList.DefaultView.Count.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void ApplyFilter()
        {

        

            if (cbFilterby.SelectedIndex == 0)
            {
                _dgvTransactionList.DefaultView.RowFilter = "";
                UpdateRecordCount();
                return;
            }

           

            string value = txtBoxInput.Text.Replace("'", "' '");
            string filter = "";

            switch (cbFilterby.Text)
            {
                case "Transaction ID":
                    if (int.TryParse(value, out _))
                        filter = $"TransactionId = {value}";
                    break;
            }

            _dgvTransactionList.DefaultView.RowFilter = filter;
            UpdateRecordCount();

        }

        

        private void txtBoxInput_TextChanged(object sender, EventArgs e)
        {
          
            ApplyFilter();
        }

        private void txtBoxInput_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (cbFilterby.Text == "Transaction ID")
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

    }
}