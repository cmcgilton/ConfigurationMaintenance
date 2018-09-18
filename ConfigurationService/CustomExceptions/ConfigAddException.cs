using System;

namespace ConfigurationManager.CustomExceptions
{
    /// <summary>
    /// Custom exception for Config Add.
    /// </summary>
    public class ConfigAddException : Exception
    {
        /// <summary>
        /// Constructor for Config Add Custom Exception.
        /// </summary>
        /// <param name="message">Message passed in the exception.</param>
        public ConfigAddException(string message) : base(message)
        {
        }
    }
}
