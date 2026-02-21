using SecureBank.Models;
using SecureBank.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureBank.Business
{
    public class clsTransactions
    {

        public static DataTable GetAllTransactions()
        {
            return clsTransactionsData.GetAllTransactions();
        }

        public static Transactions GetTransactionsByAccountID(int AccountId)
        {
            return clsTransactionsData.GetTransactionsByAccountID(AccountId);
        }

        public static bool AddTransaction(Transactions transaction)
        {
            return clsTransactionsData.AddTransaction(transaction);
        }


    }
}
