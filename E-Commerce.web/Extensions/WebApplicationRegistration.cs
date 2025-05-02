using DomainLayer.Contracts;
using E_Commerce.web.CustomMiddleWares;

namespace E_Commerce.web.Extensions
{
    public static class WebApplicationRegistration
    {
        public static async Task SeedDataBaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var serviceOfDataSeeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();

            await serviceOfDataSeeding.DataSeedAsync();
        }



        public static IApplicationBuilder UseCustomExceptionMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddelWare>();
            return app;

        }

        public static IApplicationBuilder UseSwaggerMiddleWares(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }

    }
}
