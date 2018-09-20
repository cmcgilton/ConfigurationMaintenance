
namespace ConfigurationManager
{
    /// <summary>
    /// Defines a configuration item.
    /// </summary>
    public class ConfigItem : IConfigItem
    {
        private string _value;
        private string _previousValue;

        /// <summary>
        /// Constructor for Config item.
        /// </summary>
        /// <param name="key">key to uniquely identify the item.</param>
        /// <param name="value">value of the item.</param>
        public ConfigItem(string key, string value)
        {
            this.key = key;
            _value = _previousValue = value;
        }

        /// <summary>
        /// Config item key.
        /// </summary>       
        public string key { get; set; }

        /// <summary>
        /// Config item value.
        /// </summary>
        public string value {
            get {
                return _value;
            }
            set {
                _previousValue = _value;
                _value = value;
            }
        }

        /// <summary>
        /// used to reset the value back to its previous state.
        /// </summary>
        public void resetValue()
        {
            _value = _previousValue;
        }
    }
}
