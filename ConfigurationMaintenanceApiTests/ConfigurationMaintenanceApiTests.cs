using ConfigurationMaintenanceApi.Controllers;
using ConfigurationManager;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ConfigurationMaintenanceApiTests
{
    [TestFixture]
    public class ConfigurationMaintenanceApiTests
    {
        private IConfigurationService _configurationService;
        private ConfigurationApiController _controller;

        [Test]
        public void Get_WhenCalledWithValidKey_ReturnsConfigItemSuccessfully()
        {
            // Arrange
            var configItem = new ConfigItem("key", "value");

            _configurationService.Get(configItem.key).Returns(configItem);

            // Act
            var result = _controller.Get(configItem.key);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            _configurationService.Received(1).Get(configItem.key);
        }

        [Test]
        public void Get_WhenCalledWithNoKey_ReturnsConfigItemsSuccessfully()
        {
            // Arrange
            var configItems = new List<IConfigItem>()
            {
                new ConfigItem("key1", "value1"),
                new ConfigItem("key2", "value2"),
                new ConfigItem("key3", "value3"),
            };

            _configurationService.Get().Returns(configItems);

            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            var resultString = result.Content.ReadAsStringAsync().Result;
            var results = JsonConvert.DeserializeObject<List<ConfigItem>>(resultString);

            Assert.AreEqual(configItems.Count, results.Count);            
            Assert.AreEqual(configItems[0].key, results[0].key);
            Assert.AreEqual(configItems[0].value, results[0].value);
            Assert.AreEqual(configItems[1].key, results[1].key);
            Assert.AreEqual(configItems[1].value, results[1].value);
            Assert.AreEqual(configItems[2].key, results[2].key);
            Assert.AreEqual(configItems[2].value, results[2].value);

            _configurationService.Received(1).Get();
        }

        [Test]
        public void Add_WhenCalledWithValidConfigItem_ReturnsWithOk()
        {
            // Arrange
            var configItem = new ConfigItem("key", "value");

            _configurationService.Add(configItem).Returns(true);

            // Act
            var result = _controller.Add(configItem);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(result.Content.ReadAsAsync<bool>().Result, true);
            _configurationService.Received(1).Add(configItem);
        }
        
        [Test]
        public void Update_WhenCalledWithValidConfigItem_ReturnsWithOk()
        {
            // Arrange
            var configItem = new ConfigItem("key", "value");

            _configurationService.Update(configItem).Returns(true);

            // Act
            var result = _controller.Update(configItem);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(result.Content.ReadAsAsync<bool>().Result, true);
            _configurationService.Received(1).Update(configItem);
        }

        [SetUp]
        public void Setup()
        {
            _configurationService = Substitute.For<IConfigurationService>();
            _controller = new ConfigurationApiController(_configurationService);

            _controller.Request = new HttpRequestMessage();
            _controller.Request.SetConfiguration(new HttpConfiguration());
        }
    }
}
