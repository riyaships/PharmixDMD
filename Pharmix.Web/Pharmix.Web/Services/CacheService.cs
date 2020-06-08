using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmix.Web.Services
{
    public interface ICacheService
    {
        void Set(string key, object value);
        T Get<T>(string key);
        void Remove(string key);
    }

    public class CacheService : ICacheService
    {
        private IMemoryCache _cache;

        #region Constructor

        public CacheService(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public void Set(string key, object value)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
            // Keep in cache for this time, reset time if accessed.
            .SetSlidingExpiration(TimeSpan.FromMinutes(10));

            // Save data in cache.
            _cache.Set(key, value, cacheEntryOptions);
        }

        public T Get<T>(string key)
        {
            T cacheEntry;
            _cache.TryGetValue(key, out cacheEntry);
            return cacheEntry;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        #endregion

    }
}
