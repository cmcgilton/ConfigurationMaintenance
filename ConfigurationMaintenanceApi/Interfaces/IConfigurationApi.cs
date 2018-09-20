using ConfigurationManager;
using System.Net.Http;

namespace ConfigurationMaintenanceApi
{
    public interface IConfigurationApi
    {
        /// <summary>
        /// Get all entries in the configuration.
        /// </summary>
        /// <returns>List of IConfigItem.</returns>
        HttpResponseMessage Get();

        /// <summary>
        /// Get specific entry from the configuration.
        /// </summary>
        /// <param name="key">Configuration key to search for value.</param>
        /// <returns>IConfigItem</returns>
        HttpResponseMessage Get(string key);

        /// <summary>
        /// Used to add an entry to the configuration.
        /// </summary>
        /// <param name="configItem">Config item to add.</param>
        /// <returns>true if successful.</returns>
        HttpResponseMessage Add(ConfigItem configItem);

        /// <summary>
        /// Used to update an entry in the configuration.
        /// </summary>
        /// <param name="configItem">Config item to update.</param>
        /// <returns>true if successful.</returns>
        HttpResponseMessage Update(ConfigItem configItem);       
    }
}
