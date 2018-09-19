using ConfigurationManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConfigurationManager
{
    public class XmlFileManager : IFileManager
    {
        private const string filePath = @"C:\Source\API\config.xml";
        private const string itemElement = "item";
        private const string itemsElement = "items";
        private const string keyAttribute = "key";
        private const string valueAttribute = "value";

        private IXDocumentWrapper _xDocumentWrapper;

        public XmlFileManager(IXDocumentWrapper xDocumentWrapper)
        {
            _xDocumentWrapper = xDocumentWrapper;
        }

        /// <summary>
        /// Retrieve all the config items from the xml file.
        /// </summary>
        /// <returns>List of config items.</returns>
        public List<IConfigItem> ReadAllEntriesFromFile()
        {
            var configItems = new List<IConfigItem>();

            var file = _xDocumentWrapper.Load(filePath);
            var items = file.Element(itemsElement).Elements(itemElement);
                        
            foreach(var item in items)
            {
                configItems.Add(new ConfigItem(item.Attribute(keyAttribute).Value, item.Attribute(valueAttribute).Value));
            }
                                   
            return configItems;
        }

        /// <summary>
        /// Add entry to the xml file.
        /// </summary>
        /// <param name="configItem">config item to add.</param>
        /// <returns>boolean for success/failure.</returns>
        public void AddEntry(IConfigItem configItem)
        {
            //throw new Exception("some error");

            var file = _xDocumentWrapper.Load(filePath);
            var root = file.Element(itemsElement);
            var items = root.Elements(itemElement);

            var newItem = new XElement(itemElement, new XAttribute(keyAttribute, configItem.key), new XAttribute(valueAttribute, configItem.value));
            if (items.Any())
            {
                items.Last().AddAfterSelf(newItem);
            }
            else
            {
                root.Add(newItem);
            }

            file.Save(filePath);
        }

        /// <summary>
        /// Update entry in config item.
        /// </summary>
        /// <param name="configItem">configuration item to update.</param>
        /// <returns>boolean for success/failure.</returns>
        public bool UpdateEntry(IConfigItem configItem)
        {
            //throw new Exception("some error");

            var file = _xDocumentWrapper.Load(filePath);
            var root = file.Element(itemsElement);
            var items = root.Elements(itemElement);

            if (items.Any())
            {
                var item = items.FirstOrDefault(x => x.Attribute(keyAttribute).Value == configItem.key);
                if (item != null)
                {
                    item.Attribute(valueAttribute).Value = configItem.value;
                    file.Save(filePath);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Deletes a configuration entry.
        /// </summary>
        /// <param name="configItem">Config item to delete.</param>
        /// <returns>boolean for success/failure.</returns>
        public bool DeleteEntry(IConfigItem configItem)
        {
            throw new NotImplementedException();
        }
    }
}
