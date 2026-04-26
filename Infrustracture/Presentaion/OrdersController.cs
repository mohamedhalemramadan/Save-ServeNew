using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion
{
    public class OrdersController(IServiceManager serviceManager) : ApiController
    {
        #region Create Order
        [HttpPost]
        public async Task<ActionResult<OrderResult>> Create(OrderRequest orderRequest)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await serviceManager.OrderService.CreateOrUpdateOrderAsync(orderRequest, email);
            return Ok(order);
        }
        #endregion


        #region GetOrderById
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResult>> GetOrderById(Guid id)
        {
            var Order = await serviceManager.OrderService.GetOrderByIdAsync(id);
            return Ok(Order);
        }
        #endregion
    }


}
