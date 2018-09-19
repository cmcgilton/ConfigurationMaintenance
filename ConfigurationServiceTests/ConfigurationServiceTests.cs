using NUnit.Framework;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigurationManager;
using ConfigurationManager.CustomExceptions;

namespace ConfigurationServiceTests
{
    [TestFixture]
    public class ConfigurationServiceTests
    {
        private IConfigurationService _configurationService;
        private IFileManager _fileManager;

        [Test]
        public void Get_WhenCalled_ReturnsConfigItemList()
        {
            // Arrange
            var configItems = new List<IConfigItem>()
            {
                new ConfigItem("key1", "value1"),
                new ConfigItem("key2", "value2"),
                new ConfigItem("key3", "value3"),
            };

            _fileManager.ReadAllEntriesFromFile().Returns(configItems);
            _configurationService = new ConfigurationService(_fileManager);

            // Act
            var result = _configurationService.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, configItems.Count);
            Assert.AreEqual(configItems[0].value, result.First(x => x.key == configItems[0].key).value);
            Assert.AreEqual(configItems[1].value, result.First(x => x.key == configItems[1].key).value);
            Assert.AreEqual(configItems[2].value, result.First(x => x.key == configItems[2].key).value);

            _fileManager.Received(1).ReadAllEntriesFromFile();
        }

        [Test]
        public void Get_WhenCalledWithKey_ReturnsConfigItem()
        {
            // Arrange
            var configItems = new List<IConfigItem>()
            {
                new ConfigItem("key1", "value1"),
                new ConfigItem("key2", "value2"),
                new ConfigItem("key3", "value3"),
            };

            _fileManager.ReadAllEntriesFromFile().Returns(configItems);
            _configurationService = new ConfigurationService(_fileManager);

            // Act
            var result = _configurationService.Get(configItems.First().key);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(configItems[0].key, result.key);
            Assert.AreEqual(configItems[0].value, result.value);
            
            _fileManager.Received(1).ReadAllEntriesFromFile();
        }

        [Test]        
        public void Get_WhenCalledWithInvalidKey_ThrowsKeyNotFoundException()
        {
            // Arrange
            var invalidKey = "key4";
            var configItems = new List<IConfigItem>()
            {
                new ConfigItem("key1", "value1"),
                new ConfigItem("key2", "value2"),
                new ConfigItem("key3", "value3"),
            };

            _fileManager.ReadAllEntriesFromFile().Returns(configItems);
            _configurationService = new ConfigurationService(_fileManager);

            // Act
            Assert.Throws<KeyNotFoundException>(() => _configurationService.Get(invalidKey));

            // Assert
            _fileManager.Received(1).ReadAllEntriesFromFile();
        }

        [Test]
        public void Add_WhenCalledWithExistingKey_ThrowsDuplicateKeyException()
        {
            // Arrange
            var configItems = new List<IConfigItem>()
            {
                new ConfigItem("key1", "value1"),
                new ConfigItem("key2", "value2"),
                new ConfigItem("key3", "value3"),
            };

            _fileManager.ReadAllEntriesFromFile().Returns(configItems);
            _configurationService = new ConfigurationService(_fileManager);

            // Act
            Assert.Throws<DuplicateKeyException>(() => _configurationService.Add(configItems.First()));

            // Assert
            _fileManager.Received(1).ReadAllEntriesFromFile();
            _fileManager.DidNotReceive().AddEntry(Arg.Any<ConfigItem>());
        }

        [Test]
        public void Add_WhenCalledWithNewItem_AddsItemToCacheSucessfully()
        {
            // Arrange
            var newConfigItem = new ConfigItem("key4", "value4");

            var configItems = new List<IConfigItem>()
            {
                new ConfigItem("key1", "value1"),
                new ConfigItem("key2", "value2"),
                new ConfigItem("key3", "value3"),
            };

            _fileManager.ReadAllEntriesFromFile().Returns(configItems);
            _configurationService = new ConfigurationService(_fileManager);

            // Act
            _configurationService.Add(newConfigItem);

            // Assert
            Assert.AreEqual(newConfigItem.value, _configurationService.Get(newConfigItem.key).value);
            _fileManager.Received(1).ReadAllEntriesFromFile();
            _fileManager.Received(1).AddEntry(newConfigItem);
        }

        [Test]
        public void Add_WhenCalledWhenFileManagerException_ThrowsConfigAddExceptionAndRollsBackAdd()
        {
            // Arrange
            var validItem = new ConfigItem("key4", "value4");
            var configItems = new List<IConfigItem>()
            {
                new ConfigItem("key1", "value1"),
                new ConfigItem("key2", "value2"),
                new ConfigItem("key3", "value3"),
            };

            _fileManager.ReadAllEntriesFromFile().Returns(configItems);
            _configurationService = new ConfigurationService(_fileManager);
            _fileManager.When(x => x.AddEntry(validItem)).Throw(new Exception("some error"));

            // Act
            Assert.Throws<ConfigAddException>(() => _configurationService.Add(validItem));

            // Assert
            Assert.Throws<KeyNotFoundException>(() => _configurationService.Get(validItem.key));
            _fileManager.Received(1).ReadAllEntriesFromFile();
            _fileManager.Received(1).AddEntry(validItem);
        }

        [Test]
        public void Update_WhenCalledWithInvalidKey_ThrowsKeyNotFoundException()
        {
            // Arrange
            var invalidItem = new ConfigItem("key4", "value4");
            var configItems = new List<IConfigItem>()
            {
                new ConfigItem("key1", "value1"),
                new ConfigItem("key2", "value2"),
                new ConfigItem("key3", "value3"),
            };

            _fileManager.ReadAllEntriesFromFile().Returns(configItems);
            _configurationService = new ConfigurationService(_fileManager);

            // Act
            Assert.Throws<KeyNotFoundException>(() => _configurationService.Update(invalidItem));

            // Assert
            _fileManager.Received(1).ReadAllEntriesFromFile();
            _fileManager.DidNotReceive().UpdateEntry(invalidItem);
        }

        [Test]
        public void Update_WhenCalledWhenFileManagerException_ThrowsConfigUpdateExceptionAndRollsBackUpdate()
        {
            // Arrange
            var originalValue = "value3";
            var validItem = new ConfigItem("key3", "value3new");
            var configItems = new List<IConfigItem>()
            {
                new ConfigItem("key1", "value1"),
                new ConfigItem("key2", "value2"),
                new ConfigItem("key3", originalValue),
            };

            _fileManager.ReadAllEntriesFromFile().Returns(configItems);
            _configurationService = new ConfigurationService(_fileManager);
            _fileManager.When(x => x.UpdateEntry(validItem)).Throw(new Exception("some error"));

            // Act
            Assert.Throws<ConfigUpdateException>(() => _configurationService.Update(validItem));

            // Assert
            Assert.AreEqual(originalValue, _configurationService.Get(validItem.key).value);
            _fileManager.Received(1).ReadAllEntriesFromFile();
            _fileManager.Received(1).UpdateEntry(validItem);
        }

        [Test]
        public void Update_WhenCalledWithValidItem_UpdatesItemToCacheSucessfully()
        {
            // Arrange
            var updateConfigItem = new ConfigItem("key2", "value2new");

            var configItems = new List<IConfigItem>()
            {
                new ConfigItem("key1", "value1"),
                new ConfigItem("key2", "value2"),
                new ConfigItem("key3", "value3"),
            };

            _fileManager.ReadAllEntriesFromFile().Returns(configItems);
            _configurationService = new ConfigurationService(_fileManager);

            // Act
            _configurationService.Update(updateConfigItem);

            // Assert
            Assert.AreEqual(updateConfigItem.value, _configurationService.Get(updateConfigItem.key).value);
            _fileManager.Received(1).ReadAllEntriesFromFile();
            _fileManager.Received(1).UpdateEntry(updateConfigItem);
        }
        
        [SetUp]
        public void Setup()
        {
            _fileManager = Substitute.For<IFileManager>();
        }
    }
}
