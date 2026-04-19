using Domain.Entities;

namespace Domain.Contracts;

public interface ICharityRepository : IGenericRepository<Charity, int>
{
    Task<Charity?> GetByUserIdAsync(string userId);
    Task<IEnumerable<Charity>> GetByCoverageAreaAsync(string area);
}