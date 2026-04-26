using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntities
{
    public class OrderItem : BaseEntity<Guid>
    {
        public OrderItem()
        {

        }
        public OrderItem(int productId, string name, int quantity, decimal price)
        {
            ProductId = productId;
            Name = name;
            Quantity = quantity;
            Price = price;
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
