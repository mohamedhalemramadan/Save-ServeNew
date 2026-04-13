using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    
    public class User : IdentityUser
    {
        public string DisplayName { get; set; }
        public Address Address { get; set; }

        public string Role { get; set; } // Restaurant/Consumer/Charity/Admin
        public string Phone { get; set; }
        public string Status { get; set; } // Active/Inactive
        public string AddressText { get; set; }


        // ✅ Nullable fields
        public string? NationalId { get; init; }
        public string? VehicleType { get; init; }
        public string? VehicleNumber { get; init; }
        public string? Zone { get; init; }
        public string? WorkHours { get; init; }
        public string? RegistrationNo { get; init; }
        public string? OrganizationName { get; init; }
        public string? Mission { get; init; }
        public string? CuisineType { get; init; }

        // Navigation Properties


        public Restaurant Restaurant { get; set; }
        public Charity Charity { get; set; }
        public Consumer Consumer { get; set; }
        public DeliveryPartner DeliveryPartner { get; set; }


    }
}
