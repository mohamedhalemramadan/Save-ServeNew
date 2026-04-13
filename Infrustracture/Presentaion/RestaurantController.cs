using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ApiController
    {
        private readonly IServiceManager _serviceManager;

        public RestaurantController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/restaurant
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var restaurants = await _serviceManager.RestaurantService.GetAllAsync();
                return Ok(new { success = true, data = restaurants });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // GET: api/restaurant/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var restaurant = await _serviceManager.RestaurantService.GetByIdAsync(id);
                return Ok(new { success = true, data = restaurant });
            }
            catch (Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        // GET: api/restaurant/my-restaurant
        [HttpGet("my-restaurant")]
        [Authorize(Roles = "Restaurant")]
        public async Task<IActionResult> GetMyRestaurant()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var restaurant = await _serviceManager.RestaurantService.GetByUserIdAsync(userId);
                return Ok(new { success = true, data = restaurant });
            }
            catch (Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        // GET: api/restaurant/type/Hotel
        [HttpGet("type/{type}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByType(string type)
        {
            try
            {
                var restaurants = await _serviceManager.RestaurantService.GetByTypeAsync(type);
                return Ok(new { success = true, data = restaurants });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // POST: api/restaurant
        [HttpPost]
        [Authorize(Roles = "Restaurant")]
        public async Task<IActionResult> Create([FromBody] CreateRestaurantDto dto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var restaurant = await _serviceManager.RestaurantService.CreateAsync(dto, userId);
                return CreatedAtAction(nameof(GetById), new { id = restaurant.Id },
                    new { success = true, data = restaurant });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // PUT: api/restaurant/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Restaurant")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRestaurantDto dto)
        {
            try
            {
                var restaurant = await _serviceManager.RestaurantService.UpdateAsync(id, dto);
                return Ok(new { success = true, data = restaurant });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // DELETE: api/restaurant/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _serviceManager.RestaurantService.DeleteAsync(id);
                return Ok(new { success = true, message = "Restaurant deleted successfully" });
            }
            catch (Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
    }
    }
