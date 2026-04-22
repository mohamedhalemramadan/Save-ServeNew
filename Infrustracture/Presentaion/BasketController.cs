using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController(IServiceManager ServiceManager) : ApiController
    {
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<BasketDto>> Get(string id)
        {
            var basket = await ServiceManager.BasketService.GetBasketAsync(id);
            return basket;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<BasketDto>> Update(BasketDto basketDto)
        {
            var Basket = await ServiceManager.BasketService.UpdateBasketAsync(basketDto);
            return Ok(basketDto);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await ServiceManager.BasketService.DeleteBasketAsync(id);
            return NoContent();
        }

    }
}
