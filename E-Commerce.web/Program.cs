
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using E_Commerce.web.CustomMiddleWares;
using E_Commerce.web.Extensions;
using E_Commerce.web.Factories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Presistence.Data;
using Presistence.Identity;
using Presistence.Repositories;
using Service;
using Service.MappingProfiles;
using ServiceAbstraction;
using Shared.ErrorModels;

namespace E_Commerce.web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
          
            
            #region Add Service to the container

            

             builder.Services.AddControllers();

             builder.Services.AddSwaggerServices();


             builder.Services.AddInfrastructureServices(builder.Configuration);
 
             builder.Services.AddApplicationServices();



            builder.Services.AddWebApplicationServices();


            builder.Services.AddJWTService(builder.Configuration);






            #endregion

            #region data seeding



            var app = builder.Build();

            await app.SeedDataBaseAsync();





            #endregion







            #region Middlewares

            app.UseCustomExceptionMiddleWare();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWares();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();




            #endregion






            app.Run();
        }
    }
}
