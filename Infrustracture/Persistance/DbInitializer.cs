using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistance.Dates;
using Persistance.Identity;

namespace Persistance;

public class DbInitializer : IDbInitializer
{
    private readonly StoreDBContext _storeDBContext;
    private readonly StoreIdentityContext _identityContext;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DbInitializer(
        StoreDBContext storeDBContext,
        StoreIdentityContext identityContext,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _storeDBContext = storeDBContext;
        _identityContext = identityContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitializeAsync()
    {
        await _identityContext.Database.MigrateAsync();
        await _storeDBContext.Database.MigrateAsync();
    }

    public async Task InitializeIdentityAsync()
    {
        await SeedRolesAsync();
        await SeedDefaultAdminAsync();
    }

    private async Task SeedRolesAsync()
    {
        // ✅ كل الـ roles الموجودة في المشروع
        string[] roles = { "Admin", "Restaurant", "Consumer", "Charity", "DeliveryPartner" };

        foreach (var role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    private async Task SeedDefaultAdminAsync()
    {
        if (await _userManager.Users.AnyAsync()) return;

        var admin = new User
        {
            DisplayName = "Admin",
            Email = "AdminUser@gmail.com",
            UserName = "AdminUser",
            Role = "Admin"
        };

        var result = await _userManager.CreateAsync(admin, "Passw0rd@123");
        if (result.Succeeded)
            await _userManager.AddToRoleAsync(admin, "Admin");
    }
}