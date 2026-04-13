using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Consumer
{
    public record CreateConsumerDto
    {

        public string UserID { get; init; }
        [Required]
        [Range(18, 120)]
        public int Age { get; init; }

        [Required]
        public string Gender { get; init; } // Male/Female/Other

        public string PreferredPaymentMethod { get; init; } // Cash/Card/Wallet
    }
}
