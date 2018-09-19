using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using ConfigurationManager;
using System.Threading;
using ConfigurationManager.CustomExceptions;
using System.Diagnostics;

namespace ConfigurationManager
{
    public class ConfigurationService : IConfigurationService
    {
        private IFileManager _fileManager;
        private MemoryCache _memoryCache;
        private CacheItemPolicy _cacheItemPolicy;
        private Object _lockObject;      

        public ConfigurationService(IFileManager fileManager)
        {
            _fileManager = fileManager;
            
            _lockObject = new object();
            _memoryCache = new MemoryCache("configCache");
            _cacheItemPolicy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.MaxValue
            };

            LoadValuesIntoCache();           
        }

        /// <summary>
        /// Get all entries from the configuration.
        /// </summary>
        /// <returns>Dictionary of key value pairs.</returns>
        public List<IConfigItem> Get()
        {
            var items = new List<IConfigItem>();
            foreach (var item in _memoryCache)
            {
                items.Add((IConfigItem)item.Value);
            }

            return items;
        }

        /// <summary>
        /// Get single entry from the configuration.
        /// </summary>
        /// <returns>Value associated with the key.</returns>
        public IConfigItem Get(string key)
        {
            if (!_memoryCache.Contains(key))
            {
                throw new KeyNotFoundException(string.Format("Configuration key not found {0}", key));
            }

            var configItem = (IConfigItem)_memoryCache.Get(key);
            
            return configItem;
        }

        /// <summary>
        /// Update entry in the configuration.
        /// </summary>
        /// <param name="key">key of the entry.</param>
        /// <param name="value">value to use when updating.</param>
        /// <returns>boolean of success/failure.</returns>
        public bool Update(IConfigItem configItem)
        {
            if (!_memoryCache.Contains(configItem.key))
            {
                throw new KeyNotFoundException(string.Format("Configuration key not found {0}", configItem.key));    
            }

            if (Monitor.TryEnter(_lockObject))
            {
                try
                {
                    Thread.Sleep(5000);

                    ((ConfigItem)_memoryCache[configItem.key]).value = configItem.value;
                    _fileManager.UpdateEntry(configItem);                   
                }
                catch(Exception e)
                {
                    ((ConfigItem)_memoryCache[configItem.key]).resetValue();
                    
                    throw new ConfigUpdateException(e.Message);
                }
                finally
                {
                    Monitor.Exit(_lockObject);
                }
            }
            else
            {
                throw new ResourceLockedException(string.Format("Resource locked while updating config item {0}", configItem.key));
            }

            return true;
        }

        /// <summary>
        /// Add entry into the configuration.
        /// </summary>
        /// <param name="key">key of the entry.</param>
        /// <param name="value">value to use.</param>
        /// <returns>boolean of success/failure.</returns>
        public bool Add(IConfigItem configItem)
        {
            if (_memoryCache.Contains(configItem.key))
            {
                throw new DuplicateKeyException(string.Format("Configuration key already exists {0}", configItem.key));
            }

            if (Monitor.TryEnter(_lockObject))
            {
                try
                {
                    Thread.Sleep(5000);

                    _memoryCache.Add(configItem.key, configItem, _cacheItemPolicy);
                    _fileManager.AddEntry(configItem);                                       
                }
                catch (Exception e)
                {
                    if(_memoryCache.Contains(configItem.key))
                    {
                        _memoryCache.Remove(configItem.key);
                    }

                    throw new ConfigAddException(e.Message);
                }
                finally
                {
                    Monitor.Exit(_lockObject);
                }                
            }
            else
            {
                throw new ResourceLockedException(string.Format("Resource locked while adding config item {0}", configItem.key));
            }

            return true;
        }

        /// <summary>
        /// Delete entry from the configuration.
        /// </summary>
        /// <param name="configItem">configuration item to delete.</param>
        /// <returns>boolean of success/failure.</returns>
        public bool Delete(IConfigItem configItem)
        {
            throw new NotImplementedException();
        }

        private void LoadValuesIntoCache()
        {
            if(_memoryCache.GetCount() == 0)
            {
                var config = _fileManager.ReadAllEntriesFromFile();

                foreach (var item in config)
                {
                    _memoryCache.Add(item.key, item, _cacheItemPolicy);
                }
            }                    
        }        
    }
}
