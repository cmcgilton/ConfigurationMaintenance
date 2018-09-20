using System.Xml.Linq;

namespace ConfigurationManager
{
    /// <summary>
    /// XDocument wrapper used for testing the load process.
    /// </summary>
    public class XDocumentWrapper : IXDocumentWrapper
    {
        private XDocument xDocument;

        /// <summary>
        /// Used when mocking the XDocument load process.
        /// </summary>
        /// <param name="path">path to the file.</param>
        /// <returns>Xdocument </returns>
        public XDocument Load(string path)
        {
            xDocument = XDocument.Load(path);
            return xDocument;
        }
    }
}
