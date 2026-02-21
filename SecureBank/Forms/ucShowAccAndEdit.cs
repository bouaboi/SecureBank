using SecureBank.Models;
using System;
using System.Windows.Forms;

namespace SecureBank.Forms
{
    public enum AccountFormMode { Show, Edit }
    public partial class ucShowAccAndEdit : UserControl
    {
        private Account _account;
        private AccountFormMode _AccMode;


        public ucShowAccAndEdit()
        {
            InitializeComponent();
        }

        public void LoadAccount(Account account, AccountFormMode mode)
        {
           
            _account = account;
            _AccMode = mode;

            lblAccountD.Text = account.AccountId.ToString();
            lblAccNumber.Text = account.AccountNumber;
            lblFirstName.Text = account.client.FirstName;
            lblLastName.Text = account.client.LastName;
            lblBalance.Text = account.Balance.ToString();
            lblIsActive.Text = account.IsActive ? "Active" : "Not Active";

           
        }

    }
}
