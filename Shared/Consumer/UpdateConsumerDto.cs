using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Consumer
{
    public record UpdateConsumerDto
    {
        public int? Age { get; init; }
        public string Gender { get; init; }
        public string PreferredPaymentMethod { get; init; }
    }
}
