using DomainLayer.Contracts;
using DomainLayer.Models;
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
        public void DataSeed()
        {

            try
            {

                if (_storeDbContext.Database.GetPendingMigrations().Any())
                {
                    _storeDbContext.Database.Migrate();
                }

                if (!_storeDbContext.ProductBrands.Any())
                {
                    var productBrandData = File.ReadAllText("..\\InfraStructure\\Presistence\\Data\\DataSeed\\brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(productBrandData);

                    if (brands != null && brands.Any())
                    {
                        _storeDbContext.ProductBrands.AddRange(brands);
                    }


                }

                if (!_storeDbContext.ProductTypes.Any())
                {
                    var productTypeData = File.ReadAllText("..\\InfraStructure\\Presistence\\Data\\DataSeed\\types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(productTypeData);

                    if (types != null && types.Any())
                    {
                        _storeDbContext.ProductTypes.AddRange(types);
                    }


                }




                if (!_storeDbContext.Products.Any())
                {
                    var productData = File.ReadAllText("..\\InfraStructure\\Presistence\\Data\\DataSeed\\products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productData);

                    if (products != null && products.Any())
                    {
                        _storeDbContext.Products.AddRange(products);
                    }


                }

                _storeDbContext.SaveChanges();


            }

            catch (Exception ex)
            {
               //.......
            }
        }










    }
}
