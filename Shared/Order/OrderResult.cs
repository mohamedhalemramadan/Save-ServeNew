using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Order
{
    public class OrderResult
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string OrderType { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<OrderitemDto> Items { get; set; }
        public ShippingDetailsDto ShippingDetails { get; set; }
    }
}
