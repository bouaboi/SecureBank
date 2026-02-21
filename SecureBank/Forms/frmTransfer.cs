using SecureBank.Models;
using SecureBank.Business;
using System;
using System.Windows.Forms;

namespace SecureBank.Forms
{
    public partial class frmTransfer : Form
    {
        public frmTransfer()
        {
            InitializeComponent();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {

            if (!int.TryParse(txtFromAccountId.Text, out int fromAccountId) || 
                !int.TryParse(txtToAccountId.Text, out int toAccountId))
            {
                MessageBox.Show("Please enter a valid Account ID (numbers only)");
                return;
            }


            if (!clsAccount.DoesAccountExist(fromAccountId) || !clsAccount.DoesAccountExist(toAccountId))
            {
                MessageBox.Show("Account ID not found or inactive!");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount greater than 0");
                return;
            }

            if (fromAccountId == toAccountId)
            {
                MessageBox.Show("Cannot transfer to the same account.");
                return;
            }

            Transactions transaction = new Transactions
            {
                TypeId = 3,
                Amount = amount,
                FromAccountId = fromAccountId,
                ToAccountId = toAccountId,
                Description = txtDescription.Text
            };

            bool success = clsTransactions.AddTransaction(transaction);

            if (success)
            {
                MessageBox.Show($"Transfer successful! Transaction ID: {transaction.TransactionId}");
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Transfer failed!");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ClearInputs()
        {
            txtFromAccountId.Clear();
            txtToAccountId.Clear();
            txtAmount.Clear();
            txtDescription.Clear();
        }

    }
}
