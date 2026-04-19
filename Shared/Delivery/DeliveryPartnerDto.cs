using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Delivery
{
    public record DeliveryPartnerDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string AvailabilityStatus { get; init; } = string.Empty;
        public string VehicleType { get; init; } = string.Empty;
        public string VehicleNo { get; init; } = string.Empty;
        public string CurrentLocation { get; init; } = string.Empty;
        public decimal Rating { get; init; }
    }
}
