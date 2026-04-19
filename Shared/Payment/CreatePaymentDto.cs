using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Payment
{
    public record CreatePaymentDto
    {
        public int OrderId { get; init; }
        public decimal Amount { get; init; }
        public string Method { get; init; } = string.Empty; // Cash / Card / Wallet
    }
}
