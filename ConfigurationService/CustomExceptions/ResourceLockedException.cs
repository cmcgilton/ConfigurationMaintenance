using System;

namespace ConfigurationManager.CustomExceptions
{
    /// <summary>
    /// Custom exception for when the Config Resource is locked by another action.
    /// </summary>
    public class ResourceLockedException : Exception
    {
        /// <summary>
        /// Constructor for Resource Locked Custom Exception.
        /// </summary>
        /// <param name="message">Message passed in the exception.</param>
        public ResourceLockedException(string message) : base(message)
        {
        }
    }
}
