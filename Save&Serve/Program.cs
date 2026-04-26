using Domain.Contracts;
using Domain.Entities;
using E_Commerce.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Persistance;
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
        System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        var builder = WebApplication.CreateBuilder(args);

        // 1. Infrastructure (DB + Identity + JWT)
        builder.Services.AddInfraStructureServices(builder.Configuration);


        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        });

        // 2. Repositories
        builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        builder.Services.AddScoped<IConsumerRepository, ConsumerRepository>();
        builder.Services.AddScoped<ICharityRepository, CharityRepository>();
        builder.Services.AddScoped<IDeliveryPartnerRepository, DeliveryPartnerRepository>();
        builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
        builder.Services.AddScoped<IFoodItemRepository, FoodItemRepository>();
        builder.Services.AddScoped<IBasketRepository, BasketRepository>();

        // 3. Services
        builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        builder.Services.AddScoped<IConsumerService, ConsumerService>();
        builder.Services.AddScoped<IRestaurantService, RestaurantService>();
        builder.Services.AddScoped<ICharityService, CharityService>();
        builder.Services.AddScoped<IDeliveryPartnerService, DeliveryPartnerService>();
        builder.Services.AddScoped<IFoodItemService, FoodItemService>();
        builder.Services.AddScoped<IBasketService, BasketService>();
        builder.Services.AddScoped<IPaymentService, PaymentService>();
        builder.Services.AddScoped<IServiceManager, ServiceManager>();
        builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        builder.Services.AddAutoMapper(typeof(Servcies.AssemblyRefernce).Assembly);


        // 4. Controllers
        builder.Services.AddControllers()
      .AddApplicationPart(typeof(Presentaion.AssemblyReference).Assembly);

        // 5. CORS - يقبل أي origin عشان الـ Frontend
        builder.Services.AddCors(options =>
            options.AddPolicy("AllowFrontend", policy =>
                policy.SetIsOriginAllowed(_ => true)
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials()));

        // 6. Swagger + JWT Button
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Save & Serve API", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Enter: Bearer {your_token}",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {{
                new OpenApiSecurityScheme {
                    Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                        Id   = "Bearer"
                    }
                },
                Array.Empty<string>()
            }});
        });

        var app = builder.Build();

        // 7. Seed Database (Identity Roles + Default Data)
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var initializer = services.GetRequiredService<IDbInitializer>();


                await initializer.InitializeAsync();


                await initializer.InitializeIdentityAsync();


                // await app.SeedDbAsync(); 
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }

        // 8. Middleware Pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        app.UseRouting();

        app.UseCors("AllowFrontend");
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}