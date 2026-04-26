using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Order
{
    public class OrderRequest
    {
        public string BasketId { get; init; }
        public ShippingDetailsDto shipToAddress { get; init; }
    }
}
