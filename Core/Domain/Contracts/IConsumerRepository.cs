using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IConsumerRepository : IGenericRepository<Consumer, int>
    {
        Task<Consumer?> GetByUserIdAsync(string userId);
        Task<IEnumerable<Consumer>> GetByGenderAsync(string gender);
        Task<IEnumerable<Consumer>> GetByAgeRangeAsync(int minAge, int maxAge);
    }
}
