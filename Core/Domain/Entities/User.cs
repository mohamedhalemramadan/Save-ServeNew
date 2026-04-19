using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser
{
    public string DisplayName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;

    // ✅ Optional fields - nullable
   
    public string? AddressText { get; set; } 
    public string? Zone { get; set; }
    public string? NationalId { get; set; }
    public string? VehicleType { get; set; }
    public string? VehicleNumber { get; set; }
  
    public string? WorkHours { get; set; }
    public string? RegistrationNo { get; set; }
    public string? OrganizationName { get; set; }
    public string? Mission { get; set; }
    public string? CuisineType { get; set; }

    // Navigation
    //public Address? Address { get; set; }
    //public Restaurant? Restaurant { get; set; }
    //public Charity? Charity { get; set; }
    //public Consumer? Consumer { get; set; }
    //public DeliveryPartner? DeliveryPartner { get; set; }
}