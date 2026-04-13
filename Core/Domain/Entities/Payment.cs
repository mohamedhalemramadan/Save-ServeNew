using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    
    public class Payment : BaseEntity<int>
    {
        public int OrderId { get; set; } // FK to Order
        public string Status { get; set; } // Pending/Completed/Failed
        public decimal Amount { get; set; }
        public string Method { get; set; } // Cash/Card/Wallet
        public DateTime Date { get; set; }

        // Navigation
        public Order Order { get; set; }
    }
}
