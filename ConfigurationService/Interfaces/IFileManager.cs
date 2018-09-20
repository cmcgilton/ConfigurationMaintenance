using System.Collections.Generic;

namespace ConfigurationManager
{
    /// <summary>
    /// Interface defines the File Manager contract.  
    /// </summary>
    public interface IFileManager
    {
        /// <summary>
        /// Retrieve all the config items from the xml file.
        /// </summary>
        /// <returns>List of config items.</returns>
        List<IConfigItem> ReadAllEntriesFromFile();

        /// <summary>
        /// Add entry to the xml file.
        /// </summary>
        /// <param name="configItem">config item to add.</param>
        /// <returns>boolean for success/failure.</returns>
        void AddEntry(IConfigItem configItem);

        /// <summary>
        /// Update entry in config item.
        /// </summary>
        /// <param name="configItem">configuration item to update.</param>
        /// <returns>boolean for success/failure.</returns>
        bool UpdateEntry(IConfigItem configItem);

        /// <summary>
        /// Deletes a configuration entry.
        /// </summary>
        /// <param name="configItem">Config item to delete.</param>
        /// <returns>boolean for success/failure.</returns>
        bool DeleteEntry(IConfigItem configItem);
    }
}
