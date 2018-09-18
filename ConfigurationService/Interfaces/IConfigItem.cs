using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationManager
{
    /// <summary>
    /// Defines a configuration item.
    /// </summary>
    public interface IConfigItem
    {
        /// <summary>
        /// Config item key.
        /// </summary>
        string key { get; set; }

        /// <summary>
        /// Config item value.
        /// </summary>
        string value { get; set; }

        /// <summary>
        /// used to reset the value back to its previous state.
        /// </summary>
        void resetValue();
    }
}
