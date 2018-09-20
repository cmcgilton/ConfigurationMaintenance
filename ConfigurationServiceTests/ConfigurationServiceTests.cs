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
        private IMemoryCacheService _memoryCacheService;

        [SetUp]
        public void Setup()
        {
            _fileManager = Substitute.For<IFileManager>();
            _memoryCacheService = Substitute.For<IMemoryCacheService>();
        }

        [Test]
        public void ConfigurationManager_WhenCreated_PopulatesConfigItemListFromFile()
        {
            // Arrange
            var configItems = new List<IConfigItem>()
            {
                new ConfigItem("key1", "value1"),
                new ConfigItem("key2", "value2"),
                new ConfigItem("key3", "value3"),
            };

            _memoryCacheService.GetCount().Returns(0);
            _fileManager.ReadAllEntriesFromFile().Returns(configItems);

            // Act
            _configurationService = new ConfigurationService(_fileManager, _memoryCacheService);


            // Assert
            _memoryCacheService.Received(1).Add(configItems[0]);
            _memoryCacheService.Received(1).Add(configItems[1]);
            _memoryCacheService.Received(1).Add(configItems[2]);
        }


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

            _memoryCacheService.GetAll().Returns(configItems);
            _fileManager.ReadAllEntriesFromFile().Returns(configItems);
            _configurationService = new ConfigurationService(_fileManager, _memoryCacheService);

            // Act
            var result = _configurationService.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, configItems.Count);
            Assert.AreEqual(configItems[0].value, result.First(x => x.key == configItems[0].key).value);
            Assert.AreEqual(configItems[1].value, result.First(x => x.key == configItems[1].key).value);
            Assert.AreEqual(configItems[2].value, result.First(x => x.key == configItems[2].key).value);

            _memoryCacheService.Received(1).GetAll();
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

            var item = new ConfigItem("key1", "value1");

            _memoryCacheService.Contains(item.key).Returns(true);
            _memoryCacheService.Get(item.key).Returns(item);
            _fileManager.ReadAllEntriesFromFile().Returns(configItems);
            _configurationService = new ConfigurationService(_fileManager, _memoryCacheService);

            // Act
            var result = _configurationService.Get(item.key);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(item.key, result.key);
            Assert.AreEqual(item.value, result.value);            
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

            _memoryCacheService.Contains(invalidKey).Returns(false);
            _fileManager.ReadAllEntriesFromFile().Returns(configItems);
            _configurationService = new ConfigurationService(_fileManager, _memoryCacheService);

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

            var item = new ConfigItem("key1", "value1");

            _fileManager.ReadAllEntriesFromFile().Returns(configItems);
            _memoryCacheService.Contains(item.key).Returns(true);
            _configurationService = new ConfigurationService(_fileManager, _memoryCacheService);

            // Act
            Assert.Throws<DuplicateKeyException>(() => _configurationService.Add(item));

            // Assert
            _memoryCacheService.DidNotReceive().Add(item);
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

            _memoryCacheService.Contains(newConfigItem.key).Returns(false);
            _fileManager.ReadAllEntriesFromFile().Returns(configItems);
            _configurationService = new ConfigurationService(_fileManager, _memoryCacheService);

            // Act
            _configurationService.Add(newConfigItem);

            // Assert
            _memoryCacheService.Received(1).Add(newConfigItem);
            _fileManager.Received(1).AddEntry(newConfigItem);
            _fileManager.Received(1).ReadAllEntriesFromFile();
            
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

            _memoryCacheService.Contains(validItem.key).Returns(false, true);
            _fileManager.ReadAllEntriesFromFile().Returns(configItems);
            _configurationService = new ConfigurationService(_fileManager, _memoryCacheService);
            _fileManager.When(x => x.AddEntry(validItem)).Throw(new Exception("some error"));

            // Act
            Assert.Throws<ConfigAddException>(() => _configurationService.Add(validItem));

            // Assert
            _memoryCacheService.Received(1).Add(validItem);
            _memoryCacheService.Received(1).Remove(validItem.key);
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
            _configurationService = new ConfigurationService(_fileManager, _memoryCacheService);

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

            _memoryCacheService.Contains(validItem.key).Returns(true);
            _fileManager.ReadAllEntriesFromFile().Returns(configItems);
            _configurationService = new ConfigurationService(_fileManager, _memoryCacheService);
            _fileManager.When(x => x.UpdateEntry(validItem)).Throw(new Exception("some error"));

            // Act
            Assert.Throws<ConfigUpdateException>(() => _configurationService.Update(validItem));

            // Assert
            _memoryCacheService.Received(1).Update(validItem);
            _memoryCacheService.Received(1).Reset(validItem.key);
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

            _memoryCacheService.Contains(updateConfigItem.key).Returns(true);
            _fileManager.ReadAllEntriesFromFile().Returns(configItems);
            _configurationService = new ConfigurationService(_fileManager, _memoryCacheService);

            // Act
            _configurationService.Update(updateConfigItem);

            // Assert
            _fileManager.Received(1).ReadAllEntriesFromFile();
            _memoryCacheService.Received(1).Update(updateConfigItem);
            _memoryCacheService.DidNotReceive().Reset(updateConfigItem.key);
            _fileManager.Received(1).UpdateEntry(updateConfigItem);
        }
    }
}
