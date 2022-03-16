using Microsoft.Extensions.Caching.Memory;
using System;
using WeatherApp.Models;

namespace WeatherApp.API.Helpers
{
    public class CasheHelper
    {
        private readonly IMemoryCache _memoryCache;
        public CasheHelper(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public bool GetCashe<T>(string name, out T resModel)
        {
            return _memoryCache.TryGetValue(name, out resModel);
        }

        public void SetCashe<T>(string name, T resModel, int time)
        {
            _memoryCache.Set(name, resModel, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(time)
            });
        }
    }
}
