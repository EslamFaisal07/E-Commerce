
using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Presistence.Data;
using Presistence.Repositories;
using Service;
using Service.MappingProfiles;
using ServiceAbstraction;

namespace E_Commerce.web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
          
            
            #region Add Service to the container

            

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IDataSeeding, DataSeeding>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);

            builder.Services.AddScoped<IServiceManager, ServiceManager>(); 


            #endregion

            #region data seeding


            var app = builder.Build();


            using var scope = app.Services.CreateScope();

            var serviceOfDataSeeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();

             await serviceOfDataSeeding.DataSeedAsync();


            #endregion







            #region Middlewares

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            //app.UseAuthorization();


            app.MapControllers();




            #endregion






            app.Run();
        }
    }
}
