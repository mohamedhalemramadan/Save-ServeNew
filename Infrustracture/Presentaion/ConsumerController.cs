using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.Consumer;
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
    public class ConsumerController : ApiController
    {
        private readonly IServiceManager _serviceManager;

        public ConsumerController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        [HttpGet("whoami")]
        [Authorize]
        public IActionResult WhoAmI()
        {
            return Ok(User.Claims.Select(c => new { c.Type, c.Value }));
        }

        // GET: api/consumer
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var consumers = await _serviceManager.ConsumerService.GetAllAsync();
                return Ok(new { success = true, data = consumers });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // GET: api/consumer/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var consumer = await _serviceManager.ConsumerService.GetByIdAsync(id);
                return Ok(new { success = true, data = consumer });
            }
            catch (Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        // GET: api/consumer/my-profile
        [HttpGet("my-profile")]
        [Authorize(Roles = "Consumer")]
        public async Task<IActionResult> GetMyProfile()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var consumer = await _serviceManager.ConsumerService.GetByUserIdAsync(userId!);
                return Ok(new { success = true, data = consumer });
            }
            catch (Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        // POST: api/consumer
        [HttpPost]
        [Authorize(Roles = "Consumer")]
        public async Task<IActionResult> Create([FromBody] CreateConsumerDto dto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var consumer = await _serviceManager.ConsumerService.CreateAsync(dto, userId);
                return CreatedAtAction(nameof(GetById), new { id = consumer.Id },
                    new { success = true, data = consumer });

                

            }

            catch (Exception ex)
            { 
            return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // PUT: api/consumer/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Consumer")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateConsumerDto dto)
        {
            try
            {
                var consumer = await _serviceManager.ConsumerService.UpdateAsync(id, dto);
                return Ok(new { success = true, data = consumer });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // DELETE: api/consumer/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _serviceManager.ConsumerService.DeleteAsync(id);
                return Ok(new { success = true, message = "Consumer deleted successfully" });
            }
            catch (Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}
