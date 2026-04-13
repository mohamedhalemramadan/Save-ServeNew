using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistance.Dates;
namespace Persistance
{

    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDBContext _storeDBContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(StoreDBContext storeDBContext , UserManager<User> userManager, RoleManager<IdentityRole> roleManager) 
        {
            _storeDBContext = storeDBContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitializeAsync()
        {
                //  For Update DataBase
                //Create DataBase If It Doesnot Exist And  Applying Any Pending Migrations
                if (_storeDBContext.Database.GetPendingMigrations().Any())
                await _storeDBContext.Database.MigrateAsync();
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


                // Create Users
                await _userManager.CreateAsync(AdminUser, "Passw0rd@123");
                await _userManager.CreateAsync(RestaurantUser, "Passw0rd@123");
                await _userManager.CreateAsync(ConsumerUser, "Passw0rd@123");
                await _userManager.CreateAsync(CharityUser, "Passw0rd@123");





                // Assign Roles
                await _userManager.AddToRoleAsync(AdminUser, "Admin");
                await _userManager.AddToRoleAsync(RestaurantUser, "Restaurant");
                await _userManager.AddToRoleAsync(ConsumerUser, "Consumer");
                await _userManager.AddToRoleAsync(CharityUser, "Charity");
            }


        }
    }

    
}
