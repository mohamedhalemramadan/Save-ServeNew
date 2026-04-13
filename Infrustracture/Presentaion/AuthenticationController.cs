using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;

namespace Presentaion
{
    [Route("api/[controller]")]
    public class AuthenticationController(IServiceManager serviceManager) : ApiController
    {
        #region Login
        //Authentication/Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserLoginResultDto>> Login(LoginDto loginDto)
        {
            var Result = await serviceManager.AuthenticationService.LoginAsync(loginDto);
            return Ok(Result);
        }

        #endregion
        #region Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserRegisterResultDto>> Register(UserRegisterDto userRegisterDto)
        {
            var Result = await serviceManager.AuthenticationService.RegisterAsync(userRegisterDto);
            return Ok(Result);

        }
        #endregion

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            try
            {
                //var token = await serviceManager.AuthenticationService.ForgotPasswordAsync(dto);
                var token = await serviceManager.AuthenticationService.ForgotPasswordAsync(dto);
                return Ok(new
                {
                    message = "Reset token generated",
                    token = token  // <-- token returned here
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            await serviceManager.AuthenticationService.ResetPasswordAsync(dto);
            return Ok(new { message = "Password reset successfully" });
        }
    }
}
