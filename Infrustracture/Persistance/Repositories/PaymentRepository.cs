using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.Dates;

namespace Persistance.Repositories;

public class PaymentRepository : GenericRepository<Payment, int>, IPaymentRepository
{
    private readonly StoreDBContext _db;

    public PaymentRepository(StoreDBContext db) : base(db) => _db = db;

    public async Task<Payment?> GetByOrderIdAsync(int orderId)
        => await _db.Payments
            .FirstOrDefaultAsync(p => p.OrderId == orderId);

    public async Task<IEnumerable<Payment>> GetByStatusAsync(string status)
        => await _db.Payments
            .Where(p => p.Status == status)
            .ToListAsync();
}