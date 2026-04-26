using Shared.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servcies.Abstractions
{
    public interface IOrderService
    {
        //Get Order By Id  =>
        Task<OrderResult> GetOrderByIdAsync(Guid id);




        //Create Order =>
        public Task<OrderResult> CreateOrUpdateOrderAsync(OrderRequest request, string userEmail);
    }
}
