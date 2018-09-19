using ConfigurationMaintenanceApi.Controllers;
using ConfigurationManager;
using ConfigurationManager.CustomExceptions;
using ConfigurationMaintenanceApi;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using System.Web.Http.Filters;
using System.Collections.Generic;

namespace ConfigurationMaintenanceApiTests
{
    [TestFixture]
    public class GlobalExceptionHandlerAttributeTests
    {
        private IConfigurationService _configurationService;
        private ConfigurationApiController _controller;
        private HttpActionDescriptor _httpActionDescriptor;
        private IHttpRouteData _httpRouteData;
        private HttpConfiguration _httpConfiguration;

        [Test]
        public void Controllers_WhenKeyNotFoundExceptionOccurs_ReturnsNotFoundError()
        {
            // Arrange
            var domain = "http://www.domain.com";
            var path = "/testController/testAction";
            var exceptionMessage = "exception message";
            var exception = new KeyNotFoundException(exceptionMessage);
                       
            var httpRequestMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri(domain + path)
            };

            var httpControllerContext = new HttpControllerContext(_httpConfiguration, _httpRouteData, httpRequestMessage)
            {
                Controller = _controller
            };

            var httpActionContext = new HttpActionContext(httpControllerContext, _httpActionDescriptor);
            var httpActionExecutedContext = new HttpActionExecutedContext(httpActionContext, exception);

            // Act
            var attribute = new GlobalExceptionHandlerAttribute();
            attribute.OnException(httpActionExecutedContext);

            // Assert
            var expectedError = string.Format("Exception occurred in: {0}; Exception: {1}", path, exceptionMessage);

            Assert.AreEqual(expectedError, httpActionExecutedContext.Response.Content.ReadAsStringAsync().Result);

            Assert.AreEqual(HttpStatusCode.NotFound, httpActionExecutedContext.Response.StatusCode);
        }

        [Test]
        public void Controllers_WhenDuplicateKeyExceptionOccurs_ReturnsConflictError()
        {
            // Arrange
            var domain = "http://www.domain.com";
            var path = "/testController/testAction";
            var exceptionMessage = "exception message";
            var exception = new DuplicateKeyException(exceptionMessage);

            var httpRequestMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri(domain + path)
            };

            var httpControllerContext = new HttpControllerContext(_httpConfiguration, _httpRouteData, httpRequestMessage)
            {
                Controller = _controller
            };

            var httpActionContext = new HttpActionContext(httpControllerContext, _httpActionDescriptor);
            var httpActionExecutedContext = new HttpActionExecutedContext(httpActionContext, exception);

            // Act
            var attribute = new GlobalExceptionHandlerAttribute();
            attribute.OnException(httpActionExecutedContext);

            // Assert
            var expectedError = string.Format("Exception occurred in: {0}; Exception: {1}", path, exceptionMessage);

            Assert.AreEqual(expectedError, httpActionExecutedContext.Response.Content.ReadAsStringAsync().Result);

            Assert.AreEqual(HttpStatusCode.Conflict, httpActionExecutedContext.Response.StatusCode);
        }

        [Test]
        public void Controllers_WhenConfigAddExceptionOccurs_ReturnsConflictError()
        {
            // Arrange
            var domain = "http://www.domain.com";
            var path = "/testController/testAction";
            var exceptionMessage = "exception message";
            var exception = new ConfigAddException(exceptionMessage);

            var httpRequestMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri(domain + path)
            };

            var httpControllerContext = new HttpControllerContext(_httpConfiguration, _httpRouteData, httpRequestMessage)
            {
                Controller = _controller
            };

            var httpActionContext = new HttpActionContext(httpControllerContext, _httpActionDescriptor);
            var httpActionExecutedContext = new HttpActionExecutedContext(httpActionContext, exception);

            // Act
            var attribute = new GlobalExceptionHandlerAttribute();
            attribute.OnException(httpActionExecutedContext);

            // Assert
            var expectedError = string.Format("Exception occurred in: {0}; Exception: {1}", path, exceptionMessage);

            Assert.AreEqual(expectedError, httpActionExecutedContext.Response.Content.ReadAsStringAsync().Result);

            Assert.AreEqual(HttpStatusCode.Conflict, httpActionExecutedContext.Response.StatusCode);
        }

        [Test]
        public void Controllers_WhenConfigUpdateExceptionOccurs_ReturnsConflictError()
        {
            // Arrange
            var domain = "http://www.domain.com";
            var path = "/testController/testAction";
            var exceptionMessage = "exception message";
            var exception = new ConfigUpdateException(exceptionMessage);

            var httpRequestMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri(domain + path)
            };

            var httpControllerContext = new HttpControllerContext(_httpConfiguration, _httpRouteData, httpRequestMessage)
            {
                Controller = _controller
            };

            var httpActionContext = new HttpActionContext(httpControllerContext, _httpActionDescriptor);
            var httpActionExecutedContext = new HttpActionExecutedContext(httpActionContext, exception);

            // Act
            var attribute = new GlobalExceptionHandlerAttribute();
            attribute.OnException(httpActionExecutedContext);

            // Assert
            var expectedError = string.Format("Exception occurred in: {0}; Exception: {1}", path, exceptionMessage);

            Assert.AreEqual(expectedError, httpActionExecutedContext.Response.Content.ReadAsStringAsync().Result);

            Assert.AreEqual(HttpStatusCode.Conflict, httpActionExecutedContext.Response.StatusCode);
        }

        [Test]
        public void Controllers_WhenResourceLockedExceptionOccurs_ReturnsConflictError()
        {
            // Arrange
            var domain = "http://www.domain.com";
            var path = "/testController/testAction";
            var exceptionMessage = "exception message";
            var exception = new ResourceLockedException(exceptionMessage);

            var httpRequestMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri(domain + path)
            };

            var httpControllerContext = new HttpControllerContext(_httpConfiguration, _httpRouteData, httpRequestMessage)
            {
                Controller = _controller
            };

            var httpActionContext = new HttpActionContext(httpControllerContext, _httpActionDescriptor);
            var httpActionExecutedContext = new HttpActionExecutedContext(httpActionContext, exception);

            // Act
            var attribute = new GlobalExceptionHandlerAttribute();
            attribute.OnException(httpActionExecutedContext);

            // Assert
            var expectedError = string.Format("Exception occurred in: {0}; Exception: {1}", path, exceptionMessage);

            Assert.AreEqual(expectedError, httpActionExecutedContext.Response.Content.ReadAsStringAsync().Result);

            Assert.AreEqual(HttpStatusCode.Conflict, httpActionExecutedContext.Response.StatusCode);
        }

        [Test]
        public void Controllers_WhenGeneralExceptionOccurs_ReturnsInternalServerError()
        {
            // Arrange
            var domain = "http://www.domain.com";
            var path = "/testController/testAction";
            var exceptionMessage = "exception message";
            var exception = new Exception(exceptionMessage);

            var httpRequestMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri(domain + path)
            };

            var httpControllerContext = new HttpControllerContext(_httpConfiguration, _httpRouteData, httpRequestMessage)
            {
                Controller = _controller
            };

            var httpActionContext = new HttpActionContext(httpControllerContext, _httpActionDescriptor);
            var httpActionExecutedContext = new HttpActionExecutedContext(httpActionContext, exception);

            // Act
            var attribute = new GlobalExceptionHandlerAttribute();
            attribute.OnException(httpActionExecutedContext);

            // Assert
            var expectedError = string.Format("Exception occurred in: {0}; Exception: {1}", path, exceptionMessage);

            Assert.AreEqual(expectedError, httpActionExecutedContext.Response.Content.ReadAsStringAsync().Result);

            Assert.AreEqual(HttpStatusCode.InternalServerError, httpActionExecutedContext.Response.StatusCode);
        }

        [SetUp]
        public void Setup()
        {
            _configurationService = Substitute.For<IConfigurationService>();
            _controller = new ConfigurationApiController(_configurationService);

            _httpActionDescriptor = Substitute.For<HttpActionDescriptor>();
            _httpRouteData = Substitute.For<IHttpRouteData>();
            _httpConfiguration = new HttpConfiguration();

            _controller.Request = new HttpRequestMessage();
            _controller.Request.SetConfiguration(_httpConfiguration);
        }
    }
}
