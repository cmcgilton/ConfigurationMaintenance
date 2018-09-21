using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationManager
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomConfigItem : ConfigItem, ICustomConfigItem
    {
        public CustomConfigItem(string key, string value) : base(key, value)
        {
        }

        public string value2 { get; set; }
    }
}
