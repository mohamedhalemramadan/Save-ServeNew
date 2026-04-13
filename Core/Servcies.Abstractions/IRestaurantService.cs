using Shared.Restaurant;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servcies.Abstractions
{
    public interface IRestaurantService
    {
        Task<RestaurantDto> GetByIdAsync(int id);
        Task<IEnumerable<RestaurantDto>> GetAllAsync();
        Task<RestaurantDto> GetByUserIdAsync(string userId);
        Task<RestaurantDto> CreateAsync(CreateRestaurantDto dto, string userId);
        Task<RestaurantDto> UpdateAsync(int id, UpdateRestaurantDto dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<RestaurantDto>> GetByTypeAsync(string type);
    }
}
