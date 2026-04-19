using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.Dates;

namespace Persistance.Repositories;

public class CharityRepository : GenericRepository<Charity, int>, ICharityRepository
{
    private readonly StoreDBContext _db;

    public CharityRepository(StoreDBContext db) : base(db) => _db = db;

    public async Task<Charity?> GetByUserIdAsync(string userId)
        => await _db.Charities
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.UserId == userId);

    public async Task<IEnumerable<Charity>> GetByCoverageAreaAsync(string area)
        => await _db.Charities
            .Where(c => c.CoverageArea.Contains(area))
            .Include(c => c.User)
            .ToListAsync();
}