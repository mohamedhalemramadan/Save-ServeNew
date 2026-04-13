using Domain.Contracts;


namespace E_Commerce.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> SeedDbAsync(this WebApplication app)
        {

            //Create Object From Type  That Implements IDbinitializer
            using var Scope = app.Services.CreateScope();
            var dbinitailizer = Scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbinitailizer.InitializeAsync();
            await dbinitailizer.InitializeIdentityAsync();
            return app;

        }
        
    }
}
