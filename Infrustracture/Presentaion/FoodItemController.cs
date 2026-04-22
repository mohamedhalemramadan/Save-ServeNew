using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servcies.Abstractions;
using Shared;
using Shared.Fooditem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodItemController : ControllerBase
    {
        private readonly IFoodItemService _Foodservice;

        public FoodItemController(IFoodItemService service)
        {
            _Foodservice = service;
        }

        // =========================
        // Helpers
        // =========================
        private string GetUserId()
            => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        private string GetRole()
            => User.FindFirst(ClaimTypes.Role)?.Value;

        // =========================
        // Get All (Pagination + Filter)
        // =========================
        [HttpGet]
        [Authorize(Roles = "Admin,Restaurant")]
        public async Task<IActionResult> GetAll([FromQuery] FoodItemFilterDto filter,
                                               [FromQuery] PaginationDto pagination)
        {
            var result = await _Foodservice.GetAllAsync(filter, pagination);

            return Ok(result);
        }

        // =========================
        // Get By Id
        // =========================
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _Foodservice.GetByIdAsync(id);

            return Ok(result);
        }

        // =========================
        // Create
        // =========================
        [HttpPost]
        [Authorize(Roles = "Admin,Restaurant")]
        public async Task<IActionResult> Create([FromBody] CreateFoodItemDto dto)
        {
            var result = await _Foodservice.CreateAsync(dto, GetUserId(), GetRole());

            return Ok(result);
        }

        // =========================
        // Update
        // =========================
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Restaurant")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateFoodItemDto dto)
        {
            var result = await _Foodservice.UpdateAsync(id, dto, GetUserId(), GetRole());

            return Ok(result);
        }

        // =========================
        // Delete
        // =========================
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Restaurant")]
        public async Task<IActionResult> Delete(int id)
        {
            await _Foodservice.DeleteAsync(id, GetUserId(), GetRole());

            return NoContent();
        }
    }
}
