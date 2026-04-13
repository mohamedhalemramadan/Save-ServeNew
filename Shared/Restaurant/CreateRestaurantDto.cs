using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Restaurant
{
    public record CreateRestaurantDto
    {
        [Required]
        public string OpeningHours { get; init; }

        [Required]
        public string ClosingHours { get; init; }

        [Required]
        public string Type { get; init; } // Restaurant/Hotel/Cafe

        public decimal Rating { get; init; } = 0;
    }
}
