using NUnit.Framework;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigurationManager;
using ConfigurationManager.CustomExceptions;
using System.Xml.Linq;

namespace ConfigurationServiceTests
{
    [TestFixture]
    public class XmlFileManagerTests
    {
        private IXDocumentWrapper _xDocumentWrapper;
        private IFileManager _fileManager;

        [Test]
        public void ReadAllEntriesFromFile_WhenCalled_ReturnsConfigEntries()
        {
            // Arrange
            var key1 = "key1";
            var key2 = "key2";
            var key3 = "key3";
            var key4 = "key4";
            var value1 = "value1";
            var value2 = "value2";
            var value3 = "value3";
            var value4 = "value4";

            var xml = string.Format("<items>" +
                                        "<item key=\"{0}\" value=\"{1}\" />" +
                                        "<item key=\"{2}\" value=\"{3}\" />" +
                                        "<item key=\"{4}\" value=\"{5}\" />" +
                                        "<item key=\"{6}\" value=\"{7}\" />" +
                                    "</items>", 
                                    key1, value1, key2, value2, key3, value3, key4, value4);

            var document = XDocument.Parse(xml);

            _xDocumentWrapper.Load(Arg.Any<string>()).Returns(document);
           
            // Act
            var result = _fileManager.ReadAllEntriesFromFile();

            // Assert
            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(value1, result.First(x => x.key == key1).value);
            Assert.AreEqual(value2, result.First(x => x.key == key2).value);
            Assert.AreEqual(value3, result.First(x => x.key == key3).value);
            Assert.AreEqual(value4, result.First(x => x.key == key4).value);
        }

        [SetUp]
        public void Setup()
        {
            _xDocumentWrapper = Substitute.For<IXDocumentWrapper>();
            _fileManager = new XmlFileManager(_xDocumentWrapper);
        }
    }
}
