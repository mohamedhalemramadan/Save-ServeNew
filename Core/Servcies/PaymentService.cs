using Domain.Contracts;
using Domain.Entities;
using Servcies.Abstractions;
using Shared.Payment;

namespace Servcies;

public class PaymentService : IPaymentService
{
    private readonly IUnitOfWork _uow;
    private readonly IPaymentRepository _repo;

    public PaymentService(IUnitOfWork uow, IPaymentRepository repo)
    {
        _uow = uow;
        _repo = repo;
    }

    public async Task<IEnumerable<PaymentDto>> GetAllAsync()
    {
        var items = await _repo.GetAllAsync();
        return items.Select(MapToDto);
    }

    public async Task<PaymentDto?> GetByIdAsync(int id)
    {
        var item = await _repo.GetByIdAsync(id);
        return item is null ? null : MapToDto(item);
    }

    public async Task<PaymentDto?> GetByOrderIdAsync(int orderId)
    {
        var item = await _repo.GetByOrderIdAsync(orderId);
        return item is null ? null : MapToDto(item);
    }

    public async Task<PaymentDto> CreateAsync(CreatePaymentDto dto)
    {
        var existing = await _repo.GetByOrderIdAsync(dto.OrderId);
        if (existing is not null)
            throw new InvalidOperationException("Payment already exists for this order");

        var payment = new Payment
        {
            OrderId = dto.OrderId,
            Amount = dto.Amount,
            Method = dto.Method,
            Status = "Pending",
            Date = DateTime.UtcNow
        };

        await _repo.AddAsync(payment);
        await _uow.SaveChangesAsync();
        return MapToDto(payment);
    }

    public async Task<PaymentDto> UpdateStatusAsync(int id, UpdatePaymentStatusDto dto)
    {
        var payment = await _repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Payment not found");

        payment.Status = dto.Status;

        _repo.Update(payment);
        await _uow.SaveChangesAsync();
        return MapToDto(payment);
    }

    private static PaymentDto MapToDto(Payment p) => new()
    {
        Id = p.Id,
        OrderId = p.OrderId,
        Status = p.Status,
        Amount = p.Amount,
        Method = p.Method,
        Date = p.Date
    };
}
