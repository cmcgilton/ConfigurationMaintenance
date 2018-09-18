using System.Collections.Generic;

namespace ConfigurationManager
{
    /// <summary>
    /// Interface defines the File Writer contract.  
    /// </summary>
    public interface IFileManager
    {
        List<IConfigItem> ReadAllEntriesFromFile();

        void AddEntry(IConfigItem configItem);

        bool UpdateEntry(IConfigItem configItem);

        bool DeleteEntry(IConfigItem configItem);
    }
}
