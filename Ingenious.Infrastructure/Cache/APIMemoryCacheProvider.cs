using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Infrastructure.Cache
{
    /// <summary>
    /// .Net System.Runtime.Caching
    /// MemoryCache
    /// </summary>
    public class APIMemoryCacheProvider : IAPICacheProvider
    {
        private readonly MemoryCache cache = MemoryCache.Default;

        public void Add(string key, string valKey, object value)
        {
            cache.Add(key, value, DateTimeOffset.Now.AddSeconds(GlobalMessage.CacheTimeout));
        }

        public void Add(string key, string valKey, object value,DateTimeOffset datetiemoffset)
        {
            cache.Add(key, value, datetiemoffset);
        }

        /// <summary>
        /// 更新缓存项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="valKey"></param>
        /// <param name="value"></param>
        public void Put(string key, string valKey, object value)
        {
            cache.Set(key, value, DateTimeOffset.Now.AddSeconds(GlobalMessage.CacheTimeout));
        }

        public object Get(string key, string valKey)
        {
            return cache.Get(key);
        }

        public void Remove(string key)
        {
            cache.Remove(key);
        }

        public bool Exists(string key)
        {
            return cache.Where(item => item.Key == key).Count() > 0;
        }

        public bool Exists(string key, string valKey)
        {
            return this.Exists(key);
        }

    }

}
