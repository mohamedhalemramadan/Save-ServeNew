using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.Consumer;
using System.Security.Claims;

namespace Presentaion;
[ApiController]
[Route("api/[controller]")]
public class ConsumerController : ApiController
{
    private readonly IServiceManager _serviceManager;

    public ConsumerController(IServiceManager serviceManager)
        => _serviceManager = serviceManager;

    // GET /api/consumer
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _serviceManager.ConsumerService.GetAllAsync();
        return Ok(new { success = true, data = result });
    }

    // GET /api/consumer/{id}
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _serviceManager.ConsumerService.GetByIdAsync(id);
        return result is null
            ? NotFound(new { success = false, message = "Consumer not found" })
            : Ok(new { success = true, data = result });
    }

    // GET /api/consumer/my-profile
    [HttpGet("my-profile")]
    [Authorize(Roles = "Consumer")]
    public async Task<IActionResult> GetMyProfile()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        var result = await _serviceManager.ConsumerService.GetByUserIdAsync(userId);
        return result is null
            ? NotFound(new { success = false, message = "Profile not found" })
            : Ok(new { success = true, data = result });
    }

    // POST /api/consumer
    [HttpPost]
    [Authorize(Roles = "Consumer,Admin")] // ✅ Admin كمان يقدر يعمل
    public async Task<IActionResult> Create([FromBody] CreateConsumerDto dto)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            var result = await _serviceManager.ConsumerService.CreateAsync(dto, userId);
            return CreatedAtAction(nameof(GetById), new { id = result.Id },
                new { success = true, data = result });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    // PUT /api/consumer/{id}
    [HttpPut("{id}")]
    [Authorize(Roles = "Consumer,Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateConsumerDto dto)
    {
        try
        {
            var result = await _serviceManager.ConsumerService.UpdateAsync(id, dto);
            return Ok(new { success = true, data = result });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { success = false, message = ex.Message });
        }
    }

    // DELETE /api/consumer/{id}
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _serviceManager.ConsumerService.DeleteAsync(id);
            return Ok(new { success = true, message = "Consumer deleted" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { success = false, message = ex.Message });
        }
    }
}