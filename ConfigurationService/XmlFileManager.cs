using System;
using System.Web;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ConfigurationManager
{
    /// <summary>
    /// Manager for administering xml config file.
    /// </summary>
    public class XmlFileManager : IFileManager
    {
        private const string filePath = @"C:\Source\API\config.xml";
        private const string itemElement = "item";
        private const string itemsElement = "items";
        private const string keyAttribute = "key";
        private const string valueAttribute = "value";
        private const string value2Attribute = "value2";

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
                var key = WebUtility.HtmlDecode(item.Attribute(keyAttribute).Value);
                var value = WebUtility.HtmlDecode(item.Attribute(valueAttribute).Value);

                configItems.Add(new ConfigItem(key, value));
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
            var key = WebUtility.HtmlEncode(configItem.key);
            var value = WebUtility.HtmlEncode(configItem.value);
                        
            var attributes = new List<object> {
                new XAttribute(keyAttribute, key),
                new XAttribute(valueAttribute, value)
            };
            
            if (configItem is ICustomConfigItem)
            {
                attributes.Add(new XAttribute(value2Attribute, ((ICustomConfigItem)configItem).value2));
            }

            var newItem = new XElement(itemElement, attributes.ToArray());                                       

            var file = _xDocumentWrapper.Load(filePath);
            var root = file.Element(itemsElement);
            var items = root.Elements(itemElement);
             
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
            var key = WebUtility.HtmlEncode(configItem.key);
            var value = WebUtility.HtmlEncode(configItem.value);

            var file = _xDocumentWrapper.Load(filePath);
            var root = file.Element(itemsElement);
            var items = root.Elements(itemElement);

            if (items.Any())
            {
                var item = items.FirstOrDefault(x => x.Attribute(keyAttribute).Value == key);
                if (item != null)
                {
                    item.Attribute(valueAttribute).Value = value;
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
        public bool DeleteEntry(string deleteKey)
        {
            //throw new Exception("some error");
            var key = WebUtility.HtmlEncode(deleteKey);
            
            var file = _xDocumentWrapper.Load(filePath);
            var root = file.Element(itemsElement);
            var items = root.Elements(itemElement);

            if (items.Any())
            {
                var item = items.FirstOrDefault(x => x.Attribute(keyAttribute).Value == key);
                if (item != null)
                {
                    item.Remove();
                    file.Save(filePath);
                    return true;
                }
            }

            return false;
        }
    }
}
