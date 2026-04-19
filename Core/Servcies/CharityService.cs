using Domain.Contracts;
using Domain.Entities;
using Servcies.Abstractions;
using Shared.Charity;

namespace Servcies;

public class CharityService : ICharityService
{
    private readonly IUnitOfWork _uow;
    private readonly ICharityRepository _repo;

    public CharityService(IUnitOfWork uow, ICharityRepository repo)
    {
        _uow = uow;
        _repo = repo;
    }

    public async Task<IEnumerable<CharityDto>> GetAllAsync()
    {
        var items = await _repo.GetAllAsync();
        return items.Select(MapToDto);
    }

    public async Task<CharityDto?> GetByIdAsync(int id)
    {
        var item = await _repo.GetByIdAsync(id);
        return item is null ? null : MapToDto(item);
    }

    public async Task<CharityDto?> GetByUserIdAsync(string userId)
    {
        var item = await _repo.GetByUserIdAsync(userId);
        return item is null ? null : MapToDto(item);
    }

    public async Task<IEnumerable<CharityDto>> GetByCoverageAreaAsync(string area)
    {
        var items = await _repo.GetByCoverageAreaAsync(area);
        return items.Select(MapToDto);
    }

    public async Task<CharityDto> CreateAsync(CreateCharityDto dto, string userId)
    {
        var existing = await _repo.GetByUserIdAsync(userId);
        if (existing is not null)
            throw new InvalidOperationException("User already has a charity profile");

        var charity = new Charity
        {
            UserId = userId,
            CoverageArea = dto.CoverageArea,
            RegistrationNo = dto.RegistrationNo,
            Mission = dto.Mission
        };

        await _repo.AddAsync(charity);
        await _uow.SaveChangesAsync();
        return MapToDto(charity);
    }

    public async Task<CharityDto> UpdateAsync(int id, UpdateCharityDto dto)
    {
        var charity = await _repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Charity not found");

        if (dto.CoverageArea is not null) charity.CoverageArea = dto.CoverageArea;
        if (dto.RegistrationNo is not null) charity.RegistrationNo = dto.RegistrationNo;
        if (dto.Mission is not null) charity.Mission = dto.Mission;

        _repo.Update(charity);
        await _uow.SaveChangesAsync();
        return MapToDto(charity);
    }

    public async Task DeleteAsync(int id)
    {
        var charity = await _repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Charity not found");

        _repo.Delete(charity);
        await _uow.SaveChangesAsync();
    }

    private static CharityDto MapToDto(Charity c) => new()
    {
        Id = c.Id,
        Name = c.User?.DisplayName ?? "N/A",
        CoverageArea = c.CoverageArea,
        RegistrationNo = c.RegistrationNo,
        Mission = c.Mission
    };
}