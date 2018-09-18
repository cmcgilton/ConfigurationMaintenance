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
   
        HttpResponseMessage Add(ConfigItem configItem);


        HttpResponseMessage Update(ConfigItem configItem);       
    }
}
