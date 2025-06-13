using Microsoft.Extensions.DependencyInjection;
using Service.MappingProfiles;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class ApplicationServiceRegistration
    {


        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddAutoMapper(typeof(ProductProfile).Assembly);

            services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelegate>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<Func<IProductService>>(provider=> ()=> provider.GetRequiredService<IProductService>());


            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<Func<IOrderService>>(provider => () => provider.GetRequiredService<IOrderService>());

            services.AddScoped<IAuthanticationService, AuthanticationService>();
            services.AddScoped<Func<IAuthanticationService>>(provider => () => provider.GetRequiredService<IAuthanticationService>());



            services.AddScoped<IBasketServices, BasketService>();
            services.AddScoped<Func<IBasketServices>>(provider => () => provider.GetRequiredService<IBasketServices>());

            services.AddScoped<ICachService, CachService>();

            return services;
        }
    }
}
