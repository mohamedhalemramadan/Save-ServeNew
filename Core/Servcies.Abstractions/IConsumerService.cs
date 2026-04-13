using Shared.Consumer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servcies.Abstractions
{
    public interface IConsumerService
    {
        Task<ConsumerDto> GetByIdAsync(int id);
        Task<IEnumerable<ConsumerDto>> GetAllAsync();
        Task<ConsumerDto> GetByUserIdAsync(string userId);
        Task<ConsumerDto> CreateAsync(CreateConsumerDto dto, string userId);
        Task<ConsumerDto> UpdateAsync(int id, UpdateConsumerDto dto);
        Task DeleteAsync(int id);
    }
}
