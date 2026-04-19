using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Delivery
{
    public record UpdateDeliveryPartnerDto
    {
        public string? AvailabilityStatus { get; init; }
        public string? VehicleType { get; init; }
        public string? VehicleNo { get; init; }
        public string? CurrentLocation { get; init; }
    }
}
