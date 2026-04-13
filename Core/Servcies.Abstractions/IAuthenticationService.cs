using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;


namespace Services.Abstractions
{
    public interface IAuthenticationService
    {
        Task<string> ForgotPasswordAsync(ForgotPasswordDto dto);
        public Task<UserLoginResultDto> LoginAsync(LoginDto loginDto);
        public Task<UserRegisterResultDto> RegisterAsync(UserRegisterDto registerDto);
        Task ResetPasswordAsync(ResetPasswordDto dto);
    }

}