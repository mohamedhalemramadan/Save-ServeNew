using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DeliveryPartner : BaseEntity<int>
    {
        public string UserId { get; set; } // FK to User
        public string AvailabilityStatus { get; set; } // Available/Busy
        public string VehicleType { get; set; }
        public string VehicleNo { get; set; }
        public string CurrentLocation { get; set; }
        public decimal Rating { get; set; }

        // Navigation
        public User User { get; set; }
    }
}
