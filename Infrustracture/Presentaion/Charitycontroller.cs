using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.Charity;
using System.Security.Claims;

namespace Presentaion;

[Route("api/[controller]")]
[ApiController]
public class CharityController : ApiController
{
    private readonly IServiceManager _serviceManager;

    public CharityController(IServiceManager serviceManager)
        => _serviceManager = serviceManager;

    // GET /api/charity
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var result = await _serviceManager.CharityService.GetAllAsync();
        return Ok(new { success = true, data = result });
    }

    // GET /api/charity/{id}
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _serviceManager.CharityService.GetByIdAsync(id);
        return result is null
            ? NotFound(new { success = false, message = "Charity not found" })
            : Ok(new { success = true, data = result });
    }

    // GET /api/charity/my-charity
    [HttpGet("my-charity")]
    [Authorize(Roles = "Charity")]
    public async Task<IActionResult> GetMyCharity()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        var result = await _serviceManager.CharityService.GetByUserIdAsync(userId);
        return result is null
            ? NotFound(new { success = false, message = "Charity profile not found" })
            : Ok(new { success = true, data = result });
    }

    // GET /api/charity/area/{area}
    [HttpGet("area/{area}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByCoverageArea(string area)
    {
        var result = await _serviceManager.CharityService.GetByCoverageAreaAsync(area);
        return Ok(new { success = true, data = result });
    }

    // POST /api/charity
    [HttpPost]
    [Authorize(Roles = "Charity")]
    public async Task<IActionResult> Create([FromBody] CreateCharityDto dto)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            var result = await _serviceManager.CharityService.CreateAsync(dto, userId);
            return CreatedAtAction(nameof(GetById), new { id = result.Id },
                new { success = true, data = result });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    // PUT /api/charity/{id}
    [HttpPut("{id}")]
    [Authorize(Roles = "Charity")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCharityDto dto)
    {
        try
        {
            var result = await _serviceManager.CharityService.UpdateAsync(id, dto);
            return Ok(new { success = true, data = result });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { success = false, message = ex.Message });
        }
    }

    // DELETE /api/charity/{id}
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _serviceManager.CharityService.DeleteAsync(id);
            return Ok(new { success = true, message = "Charity deleted successfully" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { success = false, message = ex.Message });
        }
    }
}