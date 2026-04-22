using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Fooditem
{
    public class CreateFoodItemDto
    {
        public string Name { get; set; }
        public string Status { get; set; } // الأفضل Enum
        public decimal Price { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int QuantityAvailable { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public decimal DiscountPercent { get; set; }


        public int? RestaurantId { get; set; }
    }
}