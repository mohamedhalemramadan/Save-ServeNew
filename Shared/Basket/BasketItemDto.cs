using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Basket
{
    public record BasketItemDto
    {
        public int Id { get; init; }
        public string ProductName { get; init; }
      
        [Range(1, double.MaxValue)]
        public decimal Price { get; init; }

        [Range(1, 98)]
        public int quantity { get; init; }
    }

}
