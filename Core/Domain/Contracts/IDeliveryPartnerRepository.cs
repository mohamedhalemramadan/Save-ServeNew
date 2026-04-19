using Domain.Entities;

namespace Domain.Contracts;

public interface IDeliveryPartnerRepository : IGenericRepository<DeliveryPartner, int>
{
    Task<DeliveryPartner?> GetByUserIdAsync(string userId);
    Task<IEnumerable<DeliveryPartner>> GetAvailableAsync();
}