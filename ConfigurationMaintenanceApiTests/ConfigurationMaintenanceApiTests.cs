using ConfigurationMaintenanceApi.Controllers;
using ConfigurationManager;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationMaintenanceApiTests
{
    [TestFixture]
    public class ConfigurationMaintenanceApiTests
    {
        private IConfigurationService _configurationService;
        private ConfigurationApiController _controller;

        [Test]
        public void Get_WhenCalled_ReturnsSuccessfully()
        {
            // Arrange

            // Act
            var result = _controller.Get("hgf");

            // Assert
            Assert.IsNotNull(result);
        }

        [SetUp]
        public void Setup()
        {
            _configurationService = Substitute.For<IConfigurationService>();
            _controller = new ConfigurationApiController(_configurationService);
        }
    }
}
