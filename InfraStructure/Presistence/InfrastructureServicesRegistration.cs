

using Microsoft.Extensions.Configuration;

namespace Presistence
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IDataSeeding, DataSeeding>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
