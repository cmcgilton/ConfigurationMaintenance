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
        public CustomConfigItem(string key, string value, string value2) : base(key, value)
        {
            this.value2 = value2;
        }

        public string value2 { get; set; }
    }
}
