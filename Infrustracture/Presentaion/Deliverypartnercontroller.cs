using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.Delivery;
using System.Security.Claims;

namespace Presentaion;

[Route("api/[controller]")]
[ApiController]
public class DeliveryPartnerController : ApiController
{
    private readonly IServiceManager _serviceManager;

    public DeliveryPartnerController(IServiceManager serviceManager)
        => _serviceManager = serviceManager;

    // GET /api/deliverypartner
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _serviceManager.DeliveryPartnerService.GetAllAsync();
        return Ok(new { success = true, data = result });
    }

    // GET /api/deliverypartner/{id}
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _serviceManager.DeliveryPartnerService.GetByIdAsync(id);
        return result is null
            ? NotFound(new { success = false, message = "Delivery partner not found" })
            : Ok(new { success = true, data = result });
    }

    // GET /api/deliverypartner/my-profile
    [HttpGet("my-profile")]
    [Authorize(Roles = "DeliveryPartner")]
    public async Task<IActionResult> GetMyProfile()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        var result = await _serviceManager.DeliveryPartnerService.GetByUserIdAsync(userId);
        return result is null
            ? NotFound(new { success = false, message = "Profile not found" })
            : Ok(new { success = true, data = result });
    }

    // GET /api/deliverypartner/available
    [HttpGet("available")]
    [Authorize(Roles = "Admin,Restaurant")]
    public async Task<IActionResult> GetAvailable()
    {
        var result = await _serviceManager.DeliveryPartnerService.GetAvailableAsync();
        return Ok(new { success = true, data = result });
    }

    // POST /api/deliverypartner
    [HttpPost]
    [Authorize(Roles = "DeliveryPartner")]
    public async Task<IActionResult> Create([FromBody] CreateDeliveryPartnerDto dto)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            var result = await _serviceManager.DeliveryPartnerService.CreateAsync(dto, userId);
            return CreatedAtAction(nameof(GetById), new { id = result.Id },
                new { success = true, data = result });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    // PUT /api/deliverypartner/{id}
    [HttpPut("{id}")]
    [Authorize(Roles = "DeliveryPartner")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateDeliveryPartnerDto dto)
    {
        try
        {
            var result = await _serviceManager.DeliveryPartnerService.UpdateAsync(id, dto);
            return Ok(new { success = true, data = result });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { success = false, message = ex.Message });
        }
    }

    // DELETE /api/deliverypartner/{id}
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _serviceManager.DeliveryPartnerService.DeleteAsync(id);
            return Ok(new { success = true, message = "Delivery partner deleted" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { success = false, message = ex.Message });
        }
    }
}