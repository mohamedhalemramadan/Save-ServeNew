using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record UserRegisterDto
    {
        public string DisplayName { get; init; }
        public string UserName { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public string PhoneNumber { get; init; }
        public string Address { get; init; }
        public string Role { get; init; }

        // ✅ Nullable fields
        public string? NationalId { get; init; }
        public string? VehicleType { get; init; }
        public string? VehicleNumber { get; init; }
        public string? Zone { get; init; }
        public string? WorkHours { get; init; }
        public string? RegistrationNo { get; init; }
        public string? OrganizationName { get; init; }
        public string? Mission { get; init; }
        public string? CuisineType { get; init ; }

    }
}
