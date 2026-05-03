using Domain.Contracts;
using Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
 

    public class BasketRepository : IBasketRepository
    {
        private readonly IMemoryCache _cache;

        public BasketRepository(IMemoryCache cache)
        {
            _cache = cache;
        }

        public Task<bool> DeleteBasketAsync(string id)
        {
            _cache.Remove(id);
            return Task.FromResult(true);
        }

        public Task<CustomerBasket?> GetBasketAsync(string id)
        {
            _cache.TryGetValue(id, out CustomerBasket? basket);
            return Task.FromResult(basket);
        }

        public Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null)
        {
            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeToLive ?? TimeSpan.FromDays(30)
            };

            _cache.Set(basket.Id, basket, options);
            return Task.FromResult<CustomerBasket?>(basket);
        }
    }


}
