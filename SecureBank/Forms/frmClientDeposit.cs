using SecureBank.Global_Classes;
using SecureBank.Models;
using SecureBank.Business;
using System;
using System.Windows.Forms;

namespace SecureBank.Forms
{
    public partial class frmClientDeposit : Form
    {

        private Account _currentAccount;
        private Client _currentClient;

        public frmClientDeposit()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {

            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount greater than 0");
                return;
            }

            Transactions transaction = new Transactions
            {
                TypeId = 1,
                Amount = amount,
                FromAccountId = null,
                ToAccountId = _currentAccount.AccountId,
                Description = txtDescription.Text
            };

            bool success = clsTransactions.AddTransaction(transaction);

            if (success)
            {
                MessageBox.Show($"Deposit successful! Transaction ID: {transaction.TransactionId}");
            }
            else
            {
                MessageBox.Show("Deposit failed!");
            }

        }

        private void frmClientDeposit_Load(object sender, EventArgs e)
        {
            LoadAccountId();
        }

        public void LoadAccountId()
        {

            _currentClient = clsSession.LoggedInClient;
            _currentAccount = clsAccount.GetAccountByClientId(_currentClient.ClientID);

            txtToAccountId.Text = _currentAccount.AccountId.ToString();
            txtToAccountId.Enabled = false;
        }
    }
}
