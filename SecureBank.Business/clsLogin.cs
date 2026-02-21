using SecureBank.Models;
using SecureBank.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureBank.Business
{
    public class clsLogin
    {
        public static User ValidateUser(string username, string passwordHash)
        {
            return clsLoginData.ValidateUser(username, passwordHash);
        }

        public static Client ValidateClient(string username, string passwordHash)
        {
            return clsLoginData.ValidateClient(username, passwordHash);
        }
    }
}
