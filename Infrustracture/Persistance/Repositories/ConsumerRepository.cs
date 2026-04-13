using Domain.Contracts;
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
    public class ConsumerRepository : GenericRepository<Consumer, int>, IConsumerRepository
    {
        private readonly StoreDBContext _storeDBContext;
        public ConsumerRepository(StoreDBContext storeDBContext)
            : base(storeDBContext)
        {
            _storeDBContext = storeDBContext;

        }

        public async Task<Consumer?> GetByUserIdAsync(string userId)
        {
            return await _storeDBContext.Consumers
                .Include(c => c.User)
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<IEnumerable<Consumer>> GetByGenderAsync(string gender)
        {
            return await _storeDBContext.Consumers
                .Where(c => c.Gender == gender)
                .Include(c => c.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Consumer>> GetByAgeRangeAsync(int minAge, int maxAge)
        {
            return await _storeDBContext.Consumers
                .Where(c => c.Age >= minAge && c.Age <= maxAge)
                .Include(c => c.User)
                .ToListAsync();
        }
    }
}
