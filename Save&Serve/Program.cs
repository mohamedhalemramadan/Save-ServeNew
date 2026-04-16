using Domain.Contracts;
using Domain.Entities;
using E_Commerce.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Persistance.Repositories;
using Persistance.Services;
using Presentaion;
using Servcies;
using Servcies.Abstractions;
using Services;
using Services.Abstractions;

namespace Save_Serve;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // ── 1. Infrastructure (DB + Identity + JWT Auth) ──────────────────
        builder.Services.AddInfraStructureServices(builder.Configuration);

        // ── 2. Repositories ───────────────────────────────────────────────
        builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        builder.Services.AddScoped<IConsumerRepository, ConsumerRepository>();

        // ── 3. Services ───────────────────────────────────────────────────
        builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        builder.Services.AddScoped<IConsumerService, ConsumerService>();
        builder.Services.AddScoped<IRestaurantService, RestaurantService>();
        builder.Services.AddScoped<IServiceManager, ServiceManager>();
        builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        // ── 4. Controllers ────────────────────────────────────────────────
        builder.Services.AddControllers()
            .AddApplicationPart(typeof(AssemblyReference).Assembly);

        // ── 5. CORS ───────────────────────────────────────────────────────
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
                policy.SetIsOriginAllowed(_ => true)
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials());
        });

        // ── 6. Swagger with JWT support ───────────────────────────────────
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Save & Serve API", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Enter: Bearer {your token}",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {{
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id   = "Bearer"
                    }
                },
                Array.Empty<string>()
            }});
        });

        var app = builder.Build();

        // ── 7. Seed DB ────────────────────────────────────────────────────
        await app.SeedDbAsync();

        // ── 8. Middleware Pipeline ─────────────────────────────────────────
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("AllowFrontend");
        app.UseAuthentication(); 
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}