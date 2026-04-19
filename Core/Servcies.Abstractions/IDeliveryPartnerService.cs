using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 using Shared.Delivery;

namespace Servcies.Abstractions
{
    public interface IDeliveryPartnerService
    {
        Task<IEnumerable<DeliveryPartnerDto>> GetAllAsync();
        Task<DeliveryPartnerDto?> GetByIdAsync(int id);
        Task<DeliveryPartnerDto?> GetByUserIdAsync(string userId);
        Task<IEnumerable<DeliveryPartnerDto>> GetAvailableAsync();
        Task<DeliveryPartnerDto> CreateAsync(CreateDeliveryPartnerDto dto, string userId);
        Task<DeliveryPartnerDto> UpdateAsync(int id, UpdateDeliveryPartnerDto dto);
        Task DeleteAsync(int id);
    }
}
