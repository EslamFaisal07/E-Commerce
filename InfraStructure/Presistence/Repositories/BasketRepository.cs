using DomainLayer.Models.BasketModule;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {

        private readonly IDatabase _database = connection.GetDatabase();

        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {
            
            var JsonBasket = JsonSerializer.Serialize(basket);
           var IsCreatedOrUpdated = await _database.StringSetAsync(basket.Id, JsonBasket, TimeToLive ?? TimeSpan.FromDays(30));

            if (IsCreatedOrUpdated)
            {
                return await GetBasketAsync(basket.Id);
                
            }
            else
            {
                return null;
            }


        }




        public async Task<bool> DeleteBasketAsync(string id)
        {
          return await _database.KeyDeleteAsync(id);



        }




        public async Task<CustomerBasket?> GetBasketAsync(string key)
        {
            
            var basket = await _database.StringGetAsync(key);
            if (basket.IsNullOrEmpty)
            {
                return null;
            }
            else
            {
                return JsonSerializer.Deserialize<CustomerBasket>(basket!);
            }


        }



    }
}
