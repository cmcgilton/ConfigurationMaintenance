using System.Xml.Linq;

namespace ConfigurationManager
{
    public class XDocumentWrapper : IXDocumentWrapper
    {
        public XDocument xDocument;

        public XDocument Load(string path)
        {
            xDocument = XDocument.Load(path);
            return xDocument;
        }
    }
}
