using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Contracts
{
   

    namespace Domain.Contracts
    {
        public interface IRestaurantRepository : IGenericRepository<Restaurant, int>
        {
            Task<Restaurant?> GetByUserIdAsync(string userId);
            Task<IEnumerable<Restaurant>> GetByTypeAsync(string type);
            Task<IEnumerable<Restaurant>> GetTopRatedAsync(int count);
        }
    }
}
