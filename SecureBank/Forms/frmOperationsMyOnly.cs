using System;
using System.Windows.Forms;

namespace SecureBank.Forms
{
    public partial class frmOperationsMyOnly : Form
    {
        public frmOperationsMyOnly()
        {
            InitializeComponent();
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
           frmClientDeposit deposit = new frmClientDeposit();
            deposit.ShowDialog();
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            frmClientWithdraw withdraw = new frmClientWithdraw();
            withdraw.ShowDialog();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            frmClientTransfer transfer = new frmClientTransfer();
            transfer.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
