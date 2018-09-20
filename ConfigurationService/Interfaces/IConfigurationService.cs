using System.Collections.Generic;

namespace ConfigurationManager
{
    /// <summary>
    /// Interface defines the Configuration Service contract. 
    /// </summary>
    public interface IConfigurationService
    {
        /// <summary>
        /// Get all entries from the configuration.
        /// </summary>
        /// <returns>List of key value pairs.</returns>
        List<IConfigItem> Get();

        /// <summary>
        /// Get single entry from the configuration.
        /// </summary>
        /// <returns>Configuration item associated with the key.</returns>
        IConfigItem Get(string key);

        /// <summary>
        /// Update entry in the configuration.
        /// </summary>
        /// <param name="configItem">configuration item to update.</param>
        /// <returns>boolean of success/failure.</returns>
        bool Update(IConfigItem configItem);

        /// <summary>
        /// Add entry into the configuration.
        /// </summary>
        /// <param name="configItem">configuration item to add.</param>
        /// <returns>boolean of success/failure.</returns>
        bool Add(IConfigItem configItem);

        /// <summary>
        /// Delete entry from the configuration.
        /// </summary>
        /// <param name="configItem">configuration item to delete.</param>
        bool Delete(IConfigItem configItem);
    }
}
