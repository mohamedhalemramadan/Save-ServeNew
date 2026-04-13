using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FoodOrder : BaseEntity<int>
    {
        public int FoodItemId { get; set; } // FK to FoodItem
        public int OrderId { get; set; } // FK to Order
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        // Navigation
        public FoodItem FoodItem { get; set; }
        public Order Order { get; set; }
    }
}
