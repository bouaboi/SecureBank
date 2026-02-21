using System;
using System.Windows.Forms;

namespace SecureBank.Forms
{
    public partial class frmOperations : Form
    {
        public frmOperations()
        {
            InitializeComponent();
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            frmDeposit operationPrgs = new frmDeposit();
            operationPrgs.ShowDialog();
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            frmWithdraw withdraw = new frmWithdraw();
            withdraw.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            frmTransfer transfer = new frmTransfer();
            transfer.ShowDialog();
        }
    }
}
