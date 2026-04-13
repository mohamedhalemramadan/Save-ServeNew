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
            // الأسطر دي بتجبر الـ Entity Framework إنه يخلّق الجداول فوراً لو مش موجودة
            // وده اللي هيمنع الـ Error بتاع 'Invalid object name AspNetRoles'
            await _identityContext.Database.EnsureCreatedAsync();
            await _storeDBContext.Database.EnsureCreatedAsync();

            if ((await _storeDBContext.Database.GetPendingMigrationsAsync()).Any())
                await _storeDBContext.Database.MigrateAsync();

            if ((await _identityContext.Database.GetPendingMigrationsAsync()).Any())
                await _identityContext.Database.MigrateAsync();
        }

        public async Task InitializeIdentityAsync()
        {
            // Seed Default Roles
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("Restaurant"));
                await _roleManager.CreateAsync(new IdentityRole("Consumer"));
                await _roleManager.CreateAsync(new IdentityRole("Charity"));
            }

            // Seed Default Users
            if (!_userManager.Users.Any())
            {
                var AdminUser = new User
                {
                    DisplayName = "AdminUser",
                    Email = "AdminUser@gmail.com",
                    UserName = "AdminUser",
                };

                var RestaurantUser = new User
                {
                    DisplayName = "RestaurantUser",
                    Email = "RestaurantUser@gmail.com",
                    UserName = "RestaurantUser",
                };

                var ConsumerUser = new User
                {
                    DisplayName = "ConsumerUser",
                    Email = "ConsumerUser@gmail.com",
                    UserName = "ConsumerUser",
                };

                var CharityUser = new User
                {
                    DisplayName = "CharityUser",
                    Email = "CharityUser@gmail.com",
                    UserName = "CharityUser",
                };

                // إنشاء المستخدمين بكلمة سر موحدة
                await _userManager.CreateAsync(AdminUser, "Passw0rd@123");
                await _userManager.CreateAsync(RestaurantUser, "Passw0rd@123");
                await _userManager.CreateAsync(ConsumerUser, "Passw0rd@123");
                await _userManager.CreateAsync(CharityUser, "Passw0rd@123");

                // ربط المستخدمين بالأدوار الخاصة بهم
                await _userManager.AddToRoleAsync(AdminUser, "Admin");
                await _userManager.AddToRoleAsync(RestaurantUser, "Restaurant");
                await _userManager.AddToRoleAsync(ConsumerUser, "Consumer");
                await _userManager.AddToRoleAsync(CharityUser, "Charity");
            }
        }
    }
}