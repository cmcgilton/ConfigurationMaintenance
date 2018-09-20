using System.Xml.Linq;

namespace ConfigurationManager
{
    /// <summary>
    /// Contract for the XDocument wrapper.
    /// </summary>
    public interface IXDocumentWrapper
    {
        /// <summary>
        /// Used when mocking the XDocument load process.
        /// </summary>
        /// <param name="path">path to the file.</param>
        /// <returns>Xdocument </returns>
        XDocument Load(string path);
    }
}
