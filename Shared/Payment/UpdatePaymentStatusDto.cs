using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Payment
{
    public record UpdatePaymentStatusDto
    {
        public string Status { get; init; } = string.Empty; // Pending / Completed / Failed
    }
}
