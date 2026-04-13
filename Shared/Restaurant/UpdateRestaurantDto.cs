using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Restaurant
{
    public record UpdateRestaurantDto
    {
        public string OpeningHours { get; init; }
        public string ClosingHours { get; init; }
        public string Type { get; init; }
        public decimal Rating { get; init; }
    }
}
