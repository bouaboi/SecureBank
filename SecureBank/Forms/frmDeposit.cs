using SecureBank.Models;
using SecureBank.Business;
using System;
using System.Windows.Forms;

namespace SecureBank.Forms
{


    public partial class frmDeposit : Form
    {

        public frmDeposit()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {

            if (!int.TryParse(txtToAccountId.Text, out int toAccountId))
            {
                MessageBox.Show("Please enter a valid Account ID (numbers only)");
                return;
            }

            if (!clsAccount.DoesAccountExist(toAccountId))
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
                TypeId = 1,  
                Amount = amount,
                FromAccountId = null,
                ToAccountId = toAccountId,
                Description = txtDescription.Text
            }; 

            bool success = clsTransactions.AddTransaction(transaction); 

            if (success)
            {
                MessageBox.Show($"Deposit successful! Transaction ID: {transaction.TransactionId}");
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Deposit failed!");
            }
        }

        private void ClearInputs()
        {
            txtToAccountId.Clear();
            txtAmount.Clear();
            txtDescription.Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
