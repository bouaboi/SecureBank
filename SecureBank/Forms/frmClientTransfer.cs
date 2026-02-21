using SecureBank.Global_Classes;
using SecureBank.Models;
using SecureBank.Business;
using System;
using System.Windows.Forms;

namespace SecureBank.Forms
{
    public partial class frmClientTransfer : Form
    {
        private Account _currentAccount;
        private Client _currentClient;

        public frmClientTransfer()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {

            if (!int.TryParse(txtToAccountId.Text, out int toAccountId))
            {
                MessageBox.Show("Please enter a valid Account ID (numbers only)");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount greater than 0");
                return;
            }

            if (!clsAccount.DoesAccountExist(toAccountId))
            {
                MessageBox.Show("Account ID not found or inactive!");
                return;
            }

            if (_currentAccount.AccountId == toAccountId)
            {
                MessageBox.Show("Cannot transfer to the same account.");
                return;
            }

            Transactions transaction = new Transactions
            {
                TypeId = 3,
                Amount = amount,
                FromAccountId = _currentAccount.AccountId,
                ToAccountId = toAccountId,
                Description = txtDescription.Text
            };

            bool success = clsTransactions.AddTransaction(transaction);

            if (success)
            {
                MessageBox.Show($"Transfer successful! Transaction ID: {transaction.TransactionId}");
            }
            else
            {
                MessageBox.Show("Transfer failed!");
            }

        }

        public void LoadAccountId()
        {

            _currentClient = clsSession.LoggedInClient;
            _currentAccount = clsAccount.GetAccountByClientId(_currentClient.ClientID);

            txtFromAccountId.Text = _currentAccount.AccountId.ToString();
            txtFromAccountId.Enabled = false;
        }

        private void frmClientTransfer_Load(object sender, EventArgs e)
        {
            LoadAccountId();
        }
    }
}
