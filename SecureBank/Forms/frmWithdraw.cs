using SecureBank.Models;
using SecureBank.Business;
using System;
using System.Windows.Forms;

namespace SecureBank.Forms
{
    public partial class frmWithdraw : Form
    {
        public frmWithdraw()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ClearInputs()
        {
            txtFromAccountId.Clear();
            txtAmount.Clear();
            txtDescription.Clear();
        }
        private void btnWithdraw_Click(object sender, EventArgs e)
        {

            if (!int.TryParse(txtFromAccountId.Text, out int fromAccountId))
            {
                MessageBox.Show("Please enter a valid Account ID (numbers only)");
                return;
            }

            if (!clsAccount.DoesAccountExist(fromAccountId))
            {
                MessageBox.Show("Account ID not found or inactive!");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount greater than 0");
                return;
            }

            Transactions transaction = new Transactions
            {
                TypeId = 2,
                Amount = amount,
                FromAccountId = fromAccountId,
                ToAccountId = null,
                Description = txtDescription.Text
            };

            bool success = clsTransactions.AddTransaction(transaction);

            if (success)
            {
                MessageBox.Show($"Withdraw successful! Transaction ID: {transaction.TransactionId}");
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Withdraw failed!");
            }
        }
    }
}
