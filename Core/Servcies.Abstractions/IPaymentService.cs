using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Shared.Payment;

namespace Servcies.Abstractions;

public interface IPaymentService
{
    Task<IEnumerable<PaymentDto>> GetAllAsync();
    Task<PaymentDto?> GetByIdAsync(int id);
    Task<PaymentDto?> GetByOrderIdAsync(int orderId);
    Task<PaymentDto> CreateAsync(CreatePaymentDto dto);
    Task<PaymentDto> UpdateStatusAsync(int id, UpdatePaymentStatusDto dto);
}