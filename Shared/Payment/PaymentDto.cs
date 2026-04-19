using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Payment
{
    public record PaymentDto
    {
        public int Id { get; init; }
        public int OrderId { get; init; }
        public string Status { get; init; } = string.Empty;
        public decimal Amount { get; init; }
        public string Method { get; init; } = string.Empty;
        public DateTime Date { get; init; }
    }
}
