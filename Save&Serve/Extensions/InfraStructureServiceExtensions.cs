using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistance;
using Persistance.Dates;
using Persistance.Identity;
using Persistance.Repositories;
using StackExchange.Redis;
using System.Security.Claims;
using System.Text;

namespace E_Commerce.Extensions;

public static class InfraStructureServiceExtensions
{
    public static IServiceCollection AddInfraStructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IDbInitializer, DbInitializer>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<StoreDBContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddDbContext<StoreIdentityContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));
        services.AddSingleton<IConnectionMultiplexer>(
               _ => ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!));

        // ✅ JWT الأول عشان يكون هو الـ default scheme
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!)),
                RoleClaimType = ClaimTypes.Role
            };
        });

       
        services.AddIdentity<User, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 8;
        })
        .AddEntityFrameworkStores<StoreIdentityContext>()
        .AddDefaultTokenProviders();

        return services;
    }
}