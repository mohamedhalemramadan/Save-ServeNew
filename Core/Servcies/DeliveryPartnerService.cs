using Domain.Contracts;
using Domain.Entities;
using Servcies.Abstractions;
using Shared.Delivery;

namespace Servcies;

public class DeliveryPartnerService : IDeliveryPartnerService
{
    private readonly IUnitOfWork _uow;
    private readonly IDeliveryPartnerRepository _repo;

    public DeliveryPartnerService(IUnitOfWork uow, IDeliveryPartnerRepository repo)
    {
        _uow = uow;
        _repo = repo;
    }

    public async Task<IEnumerable<DeliveryPartnerDto>> GetAllAsync()
    {
        var items = await _repo.GetAllAsync();
        return items.Select(MapToDto);
    }

    public async Task<DeliveryPartnerDto?> GetByIdAsync(int id)
    {
        var item = await _repo.GetByIdAsync(id);
        return item is null ? null : MapToDto(item);
    }

    public async Task<DeliveryPartnerDto?> GetByUserIdAsync(string userId)
    {
        var item = await _repo.GetByUserIdAsync(userId);
        return item is null ? null : MapToDto(item);
    }

    public async Task<IEnumerable<DeliveryPartnerDto>> GetAvailableAsync()
    {
        var items = await _repo.GetAvailableAsync();
        return items.Select(MapToDto);
    }

    public async Task<DeliveryPartnerDto> CreateAsync(CreateDeliveryPartnerDto dto, string userId)
    {
        var existing = await _repo.GetByUserIdAsync(userId);
        if (existing is not null)
            throw new InvalidOperationException("User already has a delivery partner profile");

        var partner = new DeliveryPartner
        {
            UserId = userId,
            AvailabilityStatus = dto.AvailabilityStatus,
            VehicleType = dto.VehicleType,
            VehicleNo = dto.VehicleNo,
            CurrentLocation = dto.CurrentLocation,
            Rating = 0
        };

        await _repo.AddAsync(partner);
        await _uow.SaveChangesAsync();
        return MapToDto(partner);
    }

    public async Task<DeliveryPartnerDto> UpdateAsync(int id, UpdateDeliveryPartnerDto dto)
    {
        var partner = await _repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Delivery partner not found");

        if (dto.AvailabilityStatus is not null) partner.AvailabilityStatus = dto.AvailabilityStatus;
        if (dto.VehicleType is not null) partner.VehicleType = dto.VehicleType;
        if (dto.VehicleNo is not null) partner.VehicleNo = dto.VehicleNo;
        if (dto.CurrentLocation is not null) partner.CurrentLocation = dto.CurrentLocation;

        _repo.Update(partner);
        await _uow.SaveChangesAsync();
        return MapToDto(partner);
    }

    public async Task DeleteAsync(int id)
    {
        var partner = await _repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Delivery partner not found");

        _repo.Delete(partner);
        await _uow.SaveChangesAsync();
    }

    private static DeliveryPartnerDto MapToDto(DeliveryPartner d) => new()
    {
        Id = d.Id,
        //Name = d.User?.DisplayName ?? "N/A",
        AvailabilityStatus = d.AvailabilityStatus,
        VehicleType = d.VehicleType,
        VehicleNo = d.VehicleNo,
        CurrentLocation = d.CurrentLocation,
        Rating = d.Rating
    };
}