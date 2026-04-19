using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Delivery
{

    public record CreateDeliveryPartnerDto
    {
        public string AvailabilityStatus { get; init; } = "Available";
        public string VehicleType { get; init; } = string.Empty;
        public string VehicleNo { get; init; } = string.Empty;
        public string CurrentLocation { get; init; } = string.Empty;
    }
}
