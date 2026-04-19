using Domain.Entities;

namespace Domain.Contracts;

public interface IPaymentRepository : IGenericRepository<Payment, int>
{
    Task<Payment?> GetByOrderIdAsync(int orderId);
    Task<IEnumerable<Payment>> GetByStatusAsync(string status);
}