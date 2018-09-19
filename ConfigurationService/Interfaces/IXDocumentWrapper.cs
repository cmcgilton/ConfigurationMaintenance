using System.Xml.Linq;

namespace ConfigurationManager
{
    public interface IXDocumentWrapper
    {
        XDocument Load(string path);
    }
}
