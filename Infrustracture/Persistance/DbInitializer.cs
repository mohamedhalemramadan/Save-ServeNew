using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistance.Dates;
using Persistance.Identity;

namespace Persistance
{
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

        private async Task SeedRolesAsync()
        {
            string[] roles = { "Admin", "Restaurant", "Consumer", "Charity" };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public async Task InitializeIdentityAsync()
        {
            await SeedRolesAsync();

            if (!await _userManager.Users.AnyAsync())
            {
                var admin = new User
                {
                    DisplayName = "AdminUser",
                    Email = "AdminUser@gmail.com",
                    UserName = "AdminUser"
                };

                await _userManager.CreateAsync(admin, "Passw0rd@123");

                await _userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}