using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
  
    public class Charity : BaseEntity<int>
    {
        public string UserId { get; set; } // FK to User
        public string CoverageArea { get; set; }
        public string RegistrationNo { get; set; }
        public string Mission { get; set; }

     
        public User User { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
