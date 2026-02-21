using SecureBank.DataAccess;
using SecureBank.Models;
using System.Data;

namespace SecureBank.Business
{
    public class clsAccount
    {

        public static DataTable GetAllAccounts()
        {
            return clsAccountData.GetAllAccounts();
        }

        public static int AddNewAccount(Account account)
        {
            return clsAccountData.AddNewAccount(account);
        }

        public static Account GetAccountByClientId(int ClientId)
        {
            return clsAccountData.GetAccountByClientId(ClientId);
        }

        public static bool UpdateAccount(Account account)
        {
            return clsAccountData.UpdateAccount(account);
        }


        public static bool SoftDeleteAccount(int account)
        {
            return clsAccountData.SoftDeleteAccount(account);
        }
        public static bool DoesAccountExist(int accountId)
        {
            return clsAccountData.DoesAccountExist(accountId);
        }

    }
}
