using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <returns>Dictionary of key value pairs.</returns>
        List<IConfigItem> Get();

        /// <summary>
        /// Get single entry from the configuration.
        /// </summary>
        /// <returns>Value associated with the key.</returns>
        IConfigItem Get(string key);

        /// <summary>
        /// Update entry in the configuration.
        /// </summary>
        /// <param name="configItem">configuration item to update.</param>
        bool Update(IConfigItem configItem);

        /// <summary>
        /// Add entry into the configuration.
        /// </summary>
        /// <param name="configItem">configuration item to add.</param>
        bool Add(IConfigItem configItem);

        /// <summary>
        /// Delete entry from the configuration.
        /// </summary>
        /// <param name="configItem">configuration item to delete.</param>
        bool Delete(IConfigItem configItem);
    }
}
