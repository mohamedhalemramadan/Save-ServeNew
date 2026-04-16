using Domain.Entities;

namespace Domain.Contracts;

public interface IRestaurantRepository : IGenericRepository<Restaurant, int>
{
    Task<Restaurant?> GetByUserIdAsync(string userId);
    Task<IEnumerable<Restaurant>> GetByTypeAsync(string type);
    Task<IEnumerable<Restaurant>> GetTopRatedAsync(int count);
}