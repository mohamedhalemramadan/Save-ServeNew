using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    // Domain/Entities/FoodItem.cs
    public class FoodItem : BaseEntity<int>
    {
        public int RestaurantId { get; set; } // FK to Restaurant
        public string Name { get; set; }
        public string Status { get; set; } // Available/Reserved/Expired
        public decimal Price { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int QuantityAvailable { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public decimal DiscountPercent { get; set; }

        // Navigation
        public Restaurant Restaurant { get; set; }
        public Category Category { get; set; }
        public ICollection<FoodOrder> FoodOrders { get; set; } // Mapping table
    }
}
