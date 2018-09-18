using System;

namespace ConfigurationManager.CustomExceptions
{
    /// <summary>
    /// Custom exception for Config update.
    /// </summary>
    public class ConfigUpdateException : Exception
    {
        /// <summary>
        /// Constructor for Config Update Custom Exception.
        /// </summary>
        /// <param name="message">Message passed in the exception.</param>
        public ConfigUpdateException(string message) : base(message)
        {
        }
    }
}
