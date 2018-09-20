using System;
using System.Collections.Generic;
using System.Threading;
using ConfigurationManager.CustomExceptions;

namespace ConfigurationManager
{
    /// <summary>
    /// Configuration service 
    /// </summary>
    public class ConfigurationService : IConfigurationService
    {
        private IFileManager _fileManager;
        private IMemoryCacheService _memoryCacheService;
        private Object _lockObject;      

        public ConfigurationService(IFileManager fileManager, IMemoryCacheService memoryCacheService)
        {
            _fileManager = fileManager;
            _memoryCacheService = memoryCacheService;
            _lockObject = new object();
            
            LoadValuesIntoCache();           
        }

        /// <summary>
        /// Get all entries from the configuration.
        /// </summary>
        /// <returns>Dictionary of key value pairs.</returns>
        public List<IConfigItem> Get()
        {
            return _memoryCacheService.GetAll();
        }

        /// <summary>
        /// Get single entry from the configuration.
        /// </summary>
        /// <returns>Value associated with the key.</returns>
        public IConfigItem Get(string key)
        {
            if (!_memoryCacheService.Contains(key))
            {
                throw new KeyNotFoundException(string.Format("Configuration key not found {0}", key));
            }
            
            return _memoryCacheService.Get(key);
        }

        /// <summary>
        /// Update entry in the configuration.
        /// </summary>
        /// <param name="key">key of the entry.</param>
        /// <param name="value">value to use when updating.</param>
        /// <returns>boolean of success/failure.</returns>
        public bool Update(IConfigItem configItem)
        {
            if (!_memoryCacheService.Contains(configItem.key))
            {
                throw new KeyNotFoundException(string.Format("Configuration key not found {0}", configItem.key));    
            }

            if (Monitor.TryEnter(_lockObject))
            {
                try
                {
                    Thread.Sleep(5000);

                    _memoryCacheService.Update(configItem);
                    _fileManager.UpdateEntry(configItem);                   
                }
                catch(Exception e)
                {
                    _memoryCacheService.Reset(configItem.key);
                    
                    throw new ConfigUpdateException(string.Format("Configuration update failed for key {0}", configItem.key));
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
            if (_memoryCacheService.Contains(configItem.key))
            {
                throw new DuplicateKeyException(string.Format("Configuration key already exists {0}", configItem.key));
            }

            if (Monitor.TryEnter(_lockObject))
            {
                try
                {
                    Thread.Sleep(5000);

                    _memoryCacheService.Add(configItem);
                    _fileManager.AddEntry(configItem);                                       
                }
                catch (Exception e)
                {
                    if(_memoryCacheService.Contains(configItem.key))
                    {
                        _memoryCacheService.Remove(configItem.key);
                    }

                    throw new ConfigAddException(string.Format("Configuration add failed for key {0}", configItem.key));
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
            if(_memoryCacheService.GetCount() == 0)
            {
                var config = _fileManager.ReadAllEntriesFromFile();

                foreach (var item in config)
                {
                    _memoryCacheService.Add(item);
                }
            }                    
        }        
    }
}
