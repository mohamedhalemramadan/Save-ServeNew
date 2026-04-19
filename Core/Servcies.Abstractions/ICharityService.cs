using Shared.Charity;

namespace Servcies.Abstractions;

public interface ICharityService
{
    Task<IEnumerable<CharityDto>> GetAllAsync();
    Task<CharityDto?> GetByIdAsync(int id);
    Task<CharityDto?> GetByUserIdAsync(string userId);
    Task<IEnumerable<CharityDto>> GetByCoverageAreaAsync(string area);
    Task<CharityDto> CreateAsync(CreateCharityDto dto, string userId);
    Task<CharityDto> UpdateAsync(int id, UpdateCharityDto dto);
    Task DeleteAsync(int id);
}