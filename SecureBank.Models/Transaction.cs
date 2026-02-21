using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureBank.Models
{
    public class Transactions
    {
        public int TypeId { get; set; }
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int? FromAccountId { get; set; }
        public int? ToAccountId { get; set; }
        public string TypeName { get; set; }
        public string StatusName { get; set; }
        public Account account { get; set; }
    }
}
