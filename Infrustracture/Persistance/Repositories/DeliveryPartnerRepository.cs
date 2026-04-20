using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.Dates;

namespace Persistance.Repositories;

public class DeliveryPartnerRepository : GenericRepository<DeliveryPartner, int>, IDeliveryPartnerRepository
{
    private readonly StoreDBContext _db;

    public DeliveryPartnerRepository(StoreDBContext db) : base(db) => _db = db;

    public async Task<DeliveryPartner?> GetByUserIdAsync(string userId)
        => await _db.DeliveryPartners
            //.Include(d => d.User)
            .FirstOrDefaultAsync(d => d.UserId == userId);

    public async Task<IEnumerable<DeliveryPartner>> GetAvailableAsync()
        => await _db.DeliveryPartners
            .Where(d => d.AvailabilityStatus == "Available")
            //.Include(d => d.User)
            .ToListAsync();
}