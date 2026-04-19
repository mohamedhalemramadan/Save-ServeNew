using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.Payment;

namespace Presentaion;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PaymentController : ApiController
{
    private readonly IServiceManager _serviceManager;

    public PaymentController(IServiceManager serviceManager)
        => _serviceManager = serviceManager;

    // GET /api/payment
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _serviceManager.PaymentService.GetAllAsync();
        return Ok(new { success = true, data = result });
    }

    // GET /api/payment/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _serviceManager.PaymentService.GetByIdAsync(id);
        return result is null
            ? NotFound(new { success = false, message = "Payment not found" })
            : Ok(new { success = true, data = result });
    }

    // GET /api/payment/order/{orderId}
    [HttpGet("order/{orderId}")]
    public async Task<IActionResult> GetByOrderId(int orderId)
    {
        var result = await _serviceManager.PaymentService.GetByOrderIdAsync(orderId);
        return result is null
            ? NotFound(new { success = false, message = "No payment found for this order" })
            : Ok(new { success = true, data = result });
    }

    // POST /api/payment
    [HttpPost]
    [Authorize(Roles = "Consumer")]
    public async Task<IActionResult> Create([FromBody] CreatePaymentDto dto)
    {
        try
        {
            var result = await _serviceManager.PaymentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id },
                new { success = true, data = result });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    // PATCH /api/payment/{id}/status
    [HttpPatch("{id}/status")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdatePaymentStatusDto dto)
    {
        try
        {
            var result = await _serviceManager.PaymentService.UpdateStatusAsync(id, dto);
            return Ok(new { success = true, data = result });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { success = false, message = ex.Message });
        }
    }
}