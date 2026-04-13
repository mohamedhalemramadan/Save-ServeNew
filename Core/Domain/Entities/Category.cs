using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    
    public class Category : BaseEntity<int>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        // Navigation
        public ICollection<FoodItem> FoodItems { get; set; }
    }
}
