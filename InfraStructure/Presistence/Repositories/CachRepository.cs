using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    public class CachRepository(IConnectionMultiplexer connection) : ICachRepository
    {
        readonly IDatabase _database = connection.GetDatabase();
        public async Task<string?> GetAsync(string Cachkey)
        {
            var cachValue  = await _database.StringGetAsync(Cachkey);
            return cachValue.IsNullOrEmpty ? null : cachValue.ToString();

        }

        public async Task SetAsync(string CachKey, string CahValue, TimeSpan TimeToLive)
        {
         await  _database.StringSetAsync(CachKey, CahValue, TimeToLive);
        }
    }
}
