using Domain.Contracts;
using Domain.Entities;
using Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Shared;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthenticationService(
            UserManager<User> userManager,
            IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<UserLoginResultDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                throw new Exception("Invalid email or password");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenService.GenerateToken(user, roles);

            return new UserLoginResultDto(user.DisplayName, user.Email, token, user.Role);
        }

        public async Task<UserRegisterResultDto> RegisterAsync(UserRegisterDto registerDto)
        {
            var user = new User()
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.UserName,
                Role = registerDto.Role,
                NationalId = registerDto.NationalId,
                VehicleType = registerDto.VehicleType,
                VehicleNumber = registerDto.VehicleNumber,
                Zone = registerDto.Zone ,
                WorkHours = registerDto.WorkHours ,
                RegistrationNo = registerDto.RegistrationNo,
                OrganizationName = registerDto.OrganizationName ,
                Mission = registerDto.Mission ,
                CuisineType = registerDto.CuisineType
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Registration failed: {errors}");
            }

           
             await _userManager.AddToRoleAsync(user, registerDto.Role);


            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenService.GenerateToken(user, roles);

            return new UserRegisterResultDto(user.DisplayName, user.Email, token  ,user.Role ,user.NationalId ,user.VehicleType ,user.VehicleNumber ,user.Zone ,user.WorkHours ,user.RegistrationNo ,user.OrganizationName ,user.CuisineType);
        }



        public async Task<string> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                throw new Exception("User not found");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return token;
        }

        public async Task ResetPasswordAsync(ResetPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                throw new Exception("User not found");

            var result = await _userManager.ResetPasswordAsync(
                user,
                dto.Token,
                dto.NewPassword
            );

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }
        }
    }
}