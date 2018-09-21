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
        /// Used to create a momento for the current state.
        /// </summary>
        void CreateMomento();

        /// <summary>
        /// used to reset the value back to its previous state.
        /// </summary>
        void Rollback();
    }
}
