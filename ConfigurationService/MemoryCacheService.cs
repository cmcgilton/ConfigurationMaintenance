using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace ConfigurationManager
{
    /// <summary>
    /// Service for managing in memory cache.
    /// </summary>
    public class MemoryCacheService : IMemoryCacheService
    {
        private MemoryCache _memoryCache;
        private CacheItemPolicy _cacheItemPolicy;

        public MemoryCacheService()
        {
            _memoryCache = new MemoryCache("configCache");
            _cacheItemPolicy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.MaxValue
            };
        }
                
        /// <summary>
        /// Add item to the cache.
        /// </summary>
        /// <param name="item">Configuration item to add.</param>
        /// <returns>true/false based on success.</returns>
        public bool Add(IConfigItem item)
        {
            return _memoryCache.Add(item.key, item, _cacheItemPolicy);
        }

        /// <summary>
        /// Determines whether item exists in the cache.
        /// </summary>
        /// <param name="key">key to use to search the cache.</param>
        /// <returns>true/false based on whether item is in the cache.</returns>
        public bool Contains(string key)
        {
            return _memoryCache.Contains(key);
        }

        /// <summary>
        /// Retrieve an item from the cache.
        /// </summary>
        /// <param name="key">key to use to retrieve the item.</param>
        /// <returns>Config Item.</returns>
        public IConfigItem Get(string key)
        {
            return (IConfigItem)_memoryCache.Get(key);
        }

        /// <summary>
        /// Retrieves all items from the cache.
        /// </summary>
        /// <returns>List of Config Items.</returns>
        public List<IConfigItem> GetAll()
        {
            var items = new List<IConfigItem>();
            foreach (var item in _memoryCache)
            {
                items.Add((IConfigItem)item.Value);
            }

            return items;            
        }

        /// <summary>
        /// Retrieves a count of the items in the cache.
        /// </summary>
        /// <returns>number of items.</returns>
        public long GetCount()
        {
            return _memoryCache.GetCount();
        }

        /// <summary>
        /// Removes and item from the cache.
        /// </summary>
        /// <param name="key">key to use to remove item.</param>
        /// <returns>the removed cache entry; otherwise, null.</returns>
        public object Remove(string key)
        {
            return _memoryCache.Remove(key);
        }

        /// <summary>
        /// Used to reset the cache value back to its original value.
        /// </summary>
        /// <param name="key">Item key to reset.</param>
        public void Reset(string key)
        {
            ((ConfigItem)_memoryCache[key]).resetValue();
        }

        /// <summary>
        /// Used to update the cache value.
        /// </summary>
        /// <param name="item">Config item to update.</param>
        public void Update(IConfigItem item)
        {
            ((ConfigItem)_memoryCache[item.key]).value = item.value;
        }
    }
}
