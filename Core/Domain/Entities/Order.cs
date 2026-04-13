using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
  
    public class Order : BaseEntity<int>
    {
        public int? ConsumerId { get; set; } // FK to Consumer (nullable for charity)
        public int? CharityId { get; set; } // FK to Charity (nullable for consumer)
        public decimal TotalAmount { get; set; }
        public string DeliveryAddress { get; set; }
        public string Status { get; set; } // Pending/Confirmed/Delivered/Cancelled
        public string Type { get; set; } // Commercial/Donation
        public DateTime Date { get; set; }

        // Navigation
        public Consumer Consumer { get; set; }
        //public Charity Charity { get; set; }
        //public ICollection<FoodOrder> FoodOrders { get; set; } // Mapping
        //public Payment Payment { get; set; }
    }
}
