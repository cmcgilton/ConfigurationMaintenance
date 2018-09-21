
namespace ConfigurationManager
{
    /// <summary>
    /// Defines a configuration item.
    /// </summary>
    public class ConfigItem : IConfigItem
    {
        private string _value;
        private Momento _momento;

        /// <summary>
        /// Constructor for Config item.
        /// </summary>
        /// <param name="key">key to uniquely identify the item.</param>
        /// <param name="value">value of the item.</param>
        public ConfigItem(string key, string value)
        {
            this.key = key;
            _value = value;
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
                _value = value;
            }
        }
                
        /// <summary>
        /// Used to create an instance of the current state.
        /// </summary>
        public void CreateMomento()
        {
            _momento = new Momento(_value);
        }

        /// <summary>
        /// used to reset the value back to its previous state.
        /// </summary>
        public void Rollback()
        {
            _value = _momento.State;
            _momento = null;
        }
    }
}
