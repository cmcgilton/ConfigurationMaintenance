using System.Collections.Generic;

namespace ConfigurationManager
{
    /// <summary>
    /// Contract for the Memory Cache service.
    /// </summary>
    public interface IMemoryCacheService
    {
        /// <summary>
        /// Retrieves a count of the items in the cache.
        /// </summary>
        /// <returns>number of items.</returns>
        long GetCount();

        /// <summary>
        /// Retrieves all items from the cache.
        /// </summary>
        /// <returns>List of Config Items.</returns>
        List<IConfigItem> GetAll();

        /// <summary>
        /// Retrieve an item from the cache.
        /// </summary>
        /// <param name="key">key to use to retrieve the item.</param>
        /// <returns>Config Item.</returns>
        IConfigItem Get(string key);

        /// <summary>
        /// Add item to the cache.
        /// </summary>
        /// <param name="item">Configuration item to add.</param>
        /// <returns>true/false based on success.</returns>
        bool Add(IConfigItem item);

        /// <summary>
        /// Determines whether item exists in the cache.
        /// </summary>
        /// <param name="key">key to use to search the cache.</param>
        /// <returns>true/false based on whether item is in the cache.</returns>
        bool Contains(string key);

        /// <summary>
        /// Used to update the cache value.
        /// </summary>
        /// <param name="item">Config item to update.</param>
        void Update(IConfigItem item);

        /// <summary>
        /// Removes and item from the cache.
        /// </summary>
        /// <param name="key">key to use to remove item.</param>
        /// <returns>the removed cache entry; otherwise, null.</returns>
        object Remove(string key);

        /// <summary>
        /// Used to reset the cache value back to its original value.
        /// </summary>
        /// <param name="key">Item key to reset.</param>
        void Reset(string key);
    }
}
