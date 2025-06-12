using DomainLayer.Contracts;
using DomainLayer.Models.ProductModule;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistence
{
    public class DataSeeding(StoreDbContext _storeDbContext) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {

            try
            {
                var pendingMigrations = await _storeDbContext.Database.GetPendingMigrationsAsync();

                if (pendingMigrations.Any())
                {
                  await  _storeDbContext.Database.MigrateAsync();
                }

                if (!_storeDbContext.ProductBrands.Any())
                {
                    var productBrandData =  File.OpenRead("..\\InfraStructure\\Presistence\\Data\\DataSeed\\brands.json");
                    var brands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productBrandData);

                    if (brands != null && brands.Any())
                    {
                        await _storeDbContext.ProductBrands.AddRangeAsync(brands);
                    }


                }

                if (!_storeDbContext.ProductTypes.Any())
                {
                    var productTypeData = File.OpenRead("..\\InfraStructure\\Presistence\\Data\\DataSeed\\types.json");
                    var types = await JsonSerializer.DeserializeAsync<List<ProductType>>(productTypeData);

                    if (types != null && types.Any())
                    {
                        await _storeDbContext.ProductTypes.AddRangeAsync(types);
                    }


                }




                if (!_storeDbContext.Products.Any())
                {
                    var productData = File.OpenRead("..\\InfraStructure\\Presistence\\Data\\DataSeed\\products.json");
                    var products = await JsonSerializer.DeserializeAsync<List<Product>>(productData);

                    if (products != null && products.Any())
                    {
                        await _storeDbContext.Products.AddRangeAsync(products);
                    }


                }




                if (!_storeDbContext.Set<DeliveryMethod>().Any())
                {
                    var DeliveryMethodData = File.OpenRead(@"..\InfraStructure\Presistence\Data\DataSeed\delivery.json");
                    var deliveryMethods = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(DeliveryMethodData);

                    if (deliveryMethods != null && deliveryMethods.Any())
                    {
                        await _storeDbContext.Set<DeliveryMethod>().AddRangeAsync(deliveryMethods);
                    }


                }





                await _storeDbContext.SaveChangesAsync();


            }

            catch (Exception ex)
            {
               //.......
            }
        }










    }
}
