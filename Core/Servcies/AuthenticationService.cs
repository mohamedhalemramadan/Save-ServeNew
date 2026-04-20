using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;
using Shared;
using Shared.AuthenticationDtos;

namespace Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthenticationService(UserManager<User> userManager, IJwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<UserLoginResultDto> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email)
            ?? throw new Exception("Invalid email or password");

        if (!await _userManager.CheckPasswordAsync(user, dto.Password))
            throw new Exception("Invalid email or password");

        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtTokenService.GenerateToken(user, roles);

        return new UserLoginResultDto(user.DisplayName, user.Email!, token, user.Role);
    }

    public async Task<UserRegisterResultDto> RegisterAsync(UserRegisterDto dto)
    {
        var user = new User
        {
            Email = dto.Email,
            UserName = dto.UserName,
            DisplayName = dto.DisplayName,
            PhoneNumber = dto.PhoneNumber,
            Role = dto.Role,
            AddressText = dto.Address,      
            NationalId = dto.NationalId,
            VehicleType = dto.VehicleType,
            VehicleNumber = dto.VehicleNumber,
            Zone = dto.Zone,
            WorkHours = dto.WorkHours,
            RegistrationNo = dto.RegistrationNo,
            OrganizationName = dto.OrganizationName,
            Mission = dto.Mission,
            CuisineType = dto.CuisineType
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

        await _userManager.AddToRoleAsync(user, dto.Role);

        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtTokenService.GenerateToken(user, roles);

        return new UserRegisterResultDto(
            user.DisplayName, user.Email!, token, user.Role,
            user.NationalId, user.VehicleType, user.VehicleNumber,
            user.Zone, user.WorkHours, user.RegistrationNo,
            user.OrganizationName, user.CuisineType);
    }

    public async Task<string> ForgotPasswordAsync(ForgotPasswordDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email)
            ?? throw new Exception("User not found");

        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task ResetPasswordAsync(ResetPasswordDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email)
            ?? throw new Exception("User not found");

        var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
        if (!result.Succeeded)
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
    }
    public async Task RegisterRestaurant(UserRegisterDto dto)
    {
        var user = new User { UserName = dto.Email, Email = dto.Email, DisplayName = dto.DisplayName };

        
        var result = await _userManager.CreateAsync(user, dto.Password);

        if (result.Succeeded)
        {
            
            await _userManager.AddToRoleAsync(user, "Restaurant");
        }
    }
}