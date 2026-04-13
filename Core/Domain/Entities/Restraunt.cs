using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{

    public class Restaurant : BaseEntity<int>
    {
        public string OpeningHours { get; set; }
        public string Type { get; set; } // Restaurant/Hotel/Cafe
        public decimal Rating { get; set; }
        public string ClosingHours { get; set; }

        // Navigation

        public string UserId { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;


        public ICollection<FoodItem> FoodItems { get; set; }
    }
}
