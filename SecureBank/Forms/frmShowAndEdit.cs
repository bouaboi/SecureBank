using SecureBank.Business;
using SecureBank.Models;
using System;
using System.Windows.Forms;

namespace SecureBank.Forms
{
    public partial class frmShowAndEdit : Form
    {

        private int _ClientID;
        private Client _Client;

        public frmShowAndEdit(int ClientID)
        {
            InitializeComponent();

            _ClientID = ClientID;
        }

        private void frmClientInfo_Load(object sender, EventArgs e)
        {
            _Client = clsClients.GetClientById(_ClientID);
            ucShowAndEdit1.LoadClient(_Client, ClientFormMode.Show);


        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}

