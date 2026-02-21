using SecureBank.Global_Classes;
using SecureBank.Models;
using SecureBank.Business;
using System;
using System.Windows.Forms;

namespace SecureBank.Forms
{
    public partial class frmClientWithdraw : Form
    {
        private Account _currentAccount;
        private Client _currentClient;

        public frmClientWithdraw()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount greater than 0");
                return;
            }

            Transactions transaction = new Transactions
            {
                TypeId = 2,
                Amount = amount,
                FromAccountId = _currentAccount.AccountId,
                ToAccountId = null,
                Description = txtDescription.Text
            };

            bool success = clsTransactions.AddTransaction(transaction);

            if (success)
            {
                MessageBox.Show($"Withdraw successful! Transaction ID: {transaction.TransactionId}");
            }
            else
            {
                MessageBox.Show("Withdraw failed!");
            }

        }

        public void LoadAccountId()
        {

            _currentClient = clsSession.LoggedInClient;
            _currentAccount = clsAccount.GetAccountByClientId(_currentClient.ClientID);

            txtFromAccountId.Text = _currentAccount.AccountId.ToString();
            txtFromAccountId.Enabled = false;
        }

        private void frmClientWithdraw_Load(object sender, EventArgs e)
        {
            LoadAccountId();
        }
    }
}
