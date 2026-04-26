using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntities
{

    public class Order : BaseEntity<Guid>
    {


        public Order()
        {

        }

        //-----------------------
        public Order(string _UserId, shippingDetails _ShippingDetails, List<OrderItem> _orderItems, decimal _Subtotal)
        {
            Id = Guid.NewGuid();
            UserId = _UserId;
            ShippingDetails = _ShippingDetails;
            Items = _orderItems;
            TotalPrice = _Subtotal;

        }

        public string UserId { get; set; }
        public string? OrderType { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<OrderItem> Items { get; set; }
        public shippingDetails ShippingDetails { get; set; }



    }
}
