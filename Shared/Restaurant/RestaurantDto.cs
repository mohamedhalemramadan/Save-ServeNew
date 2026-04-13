using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record RestaurantDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string OpeningHours { get; init; }
        public string ClosingHours { get; init; }
        public string Type { get; init; }
        public decimal Rating { get; init; }
    }
}
