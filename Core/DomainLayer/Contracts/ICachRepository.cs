using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface ICachRepository
    {
        Task<string?> GetAsync(string Cachkey);

        Task SetAsync(string CachKey , string CahValue , TimeSpan TimeToLive);

    }
}
