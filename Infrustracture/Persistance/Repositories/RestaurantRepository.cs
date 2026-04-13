using Domain.Contracts;
using Domain.Contracts.Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.Dates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class RestaurantRepository : GenericRepository<Restaurant, int>, IRestaurantRepository
    {
        private readonly StoreDBContext _storeDBContext;
        public RestaurantRepository(StoreDBContext storeDBContext)
            : base(storeDBContext)
        {
            _storeDBContext = storeDBContext;
        }

        public async Task<Restaurant?> GetByUserIdAsync(string userId)
        {
            return await _storeDBContext.Restaurants
                .Include(r => r.User)
                .Include(r => r.FoodItems)
                .FirstOrDefaultAsync(r => r.UserId == userId);
        }

        public async Task<IEnumerable<Restaurant>> GetByTypeAsync(string type)
        {
            return await _storeDBContext.Restaurants
                .Where(r => r.Type == type)
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Restaurant>> GetTopRatedAsync(int count)
        {
            return await _storeDBContext.Restaurants
                .OrderByDescending(r => r.Rating)
                .Take(count)
                .Include(r => r.User)
                .ToListAsync();
        }
    }
}

