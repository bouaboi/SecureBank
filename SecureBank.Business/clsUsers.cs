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
    public class clsUsers
    {
        public static DataTable GetAllAccounts()
        {
            return clsUserData.GetAllAccounts();
        }

        public static bool AddUser(User user)
        {
            return clsUserData.AddUser(user);
        }
        
    }
}
