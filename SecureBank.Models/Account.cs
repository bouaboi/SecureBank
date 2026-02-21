using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureBank.Models
{
    public class Account
    {

        public int AccountId { get; set; }
        public string AccountNumber { get; set; }
        public string PinCodeHash { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
        public Client client { get; set; }

    }
}
