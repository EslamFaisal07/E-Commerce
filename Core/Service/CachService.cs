using DomainLayer.Contracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service
{
    internal class CachService(ICachRepository cachRepository) : ICachService
    {
        public async Task<string?> GetAsync(string cachKey)
        {
           return await cachRepository.GetAsync(cachKey);
        }

        public async Task SetAsync(string cachKey, object cachValue, TimeSpan timeToLive)
        {
            var value = JsonSerializer.Serialize(cachValue);
           await cachRepository.SetAsync(cachKey, value, timeToLive);
        }



    }
}
