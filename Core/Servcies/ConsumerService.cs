using Domain.Contracts;
using Domain.Entities;
using Servcies.Abstractions;
using Shared.Consumer;

namespace Servcies;

public class ConsumerService : IConsumerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConsumerRepository _consumerRepository;

    public ConsumerService(IUnitOfWork unitOfWork, IConsumerRepository consumerRepository)
    {
        _unitOfWork = unitOfWork;
        _consumerRepository = consumerRepository;
    }

    public async Task<ConsumerDto?> GetByIdAsync(int id)
    {
        var consumer = await _consumerRepository.GetByIdAsync(id);
        return consumer is null ? null : MapToDto(consumer);
    }

    public async Task<IEnumerable<ConsumerDto>> GetAllAsync()
    {
        var consumers = await _consumerRepository.GetAllAsync();
        return consumers.Select(MapToDto);
    }

    public async Task<ConsumerDto?> GetByUserIdAsync(string userId)
    {
        var consumer = await _consumerRepository.GetByUserIdAsync(userId);
        return consumer is null ? null : MapToDto(consumer);
    }

    public async Task<ConsumerDto> CreateAsync(CreateConsumerDto dto, string userId)
    {
        var existing = await _consumerRepository.GetByUserIdAsync(userId);
        if (existing is not null)
            throw new InvalidOperationException("User already has a consumer profile");

        var consumer = new Consumer
        {
            UserId = userId,
            Age = dto.Age,
            Gender = dto.Gender,
            PreferredPaymentMethod = dto.PreferredPaymentMethod
        };

        await _consumerRepository.AddAsync(consumer);
        await _unitOfWork.SaveChangesAsync();
        return MapToDto(consumer);
    }

    public async Task<ConsumerDto> UpdateAsync(int id, UpdateConsumerDto dto)
    {
        var consumer = await _consumerRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Consumer not found");

        if (dto.Age.HasValue) consumer.Age = dto.Age.Value;
        if (dto.Gender is not null) consumer.Gender = dto.Gender;
        if (dto.PreferredPaymentMethod is not null) consumer.PreferredPaymentMethod = dto.PreferredPaymentMethod;

        _consumerRepository.Update(consumer);
        await _unitOfWork.SaveChangesAsync();
        return MapToDto(consumer);
    }

    public async Task DeleteAsync(int id)
    {
        var consumer = await _consumerRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Consumer not found");

        _consumerRepository.Delete(consumer);
        await _unitOfWork.SaveChangesAsync();
    }

    private static ConsumerDto MapToDto(Consumer c) => new()
    {
        Id = c.Id,
        //Name = c.User?.DisplayName ?? "N/A",
        Age = c.Age,
        Gender = c.Gender,
        PreferredPaymentMethod = c.PreferredPaymentMethod
    };
}