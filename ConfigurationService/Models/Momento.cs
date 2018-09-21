
namespace ConfigurationManager
{
    /// <summary>
    /// Momento class to store the state of original config item.
    /// </summary>
    public class Momento
    {
        private string _state;

        public Momento(string state)
        {
            _state = state;
        }

        /// <summary>
        /// Stores the previous state.
        /// </summary>
        public string State
        {
            get { return _state; }
        }
    }
}
