using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record UserRegisterResultDto(string DisplayName, string Email, string Token, string role , string NationalId , 
        string VehicleType , string VehicleNumber , string Zone ,
        string WorkHours , string RegistrationNo , string OrganizationName , string CuisineType ) 
    {

         
    }
}
