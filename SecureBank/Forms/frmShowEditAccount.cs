using SecureBank.Models;
using SecureBank.Business;
using System;
using System.Windows.Forms;

namespace SecureBank.Forms
{
    public partial class frmShowEditAccount : Form
    {
        private int _ClientId;
        private Account _Account;

        public frmShowEditAccount(int ClientId)
        {
            InitializeComponent();
            _ClientId = ClientId;

        }

        private void frmShowEditAccount_Load(object sender, EventArgs e)
        {
            _Account = clsAccount.GetAccountByClientId(_ClientId);
            ucShowAccAndEdit1.LoadAccount(_Account, AccountFormMode.Show);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }

}
