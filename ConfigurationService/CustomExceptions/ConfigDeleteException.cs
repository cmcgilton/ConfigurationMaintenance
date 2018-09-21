using System;

namespace ConfigurationManager.CustomExceptions
{
    /// <summary>
    /// Custom exception for Config delete.
    /// </summary>
    public class ConfigDeleteException : Exception
    {
        /// <summary>
        /// Constructor for Config Delete Custom Exception.
        /// </summary>
        /// <param name="message">Message passed in the exception.</param>
        public ConfigDeleteException(string message) : base(message)
        {
        }
    }
}
