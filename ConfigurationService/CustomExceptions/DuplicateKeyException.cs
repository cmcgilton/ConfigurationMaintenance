using System;

namespace ConfigurationManager.CustomExceptions
{
    /// <summary>
    /// Custom exception for Duplicate key.
    /// </summary>
    public class DuplicateKeyException : Exception
    {
        /// <summary>
        /// Constructor for Duplicate Key Custom Exception.
        /// </summary>
        /// <param name="message">Message passed in the exception.</param>
        public DuplicateKeyException(string message) : base(message)
        {
        }
    }
}
