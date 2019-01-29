using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
using ConfigurationManager;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using ConfigurationMaintenanceApi.CustomModelBinders;

namespace ConfigurationMaintenanceApi.Controllers
{
    /// <summary>
    /// Api Controller for the configuration maintenance.
    /// </summary>
    [RoutePrefix("api/configuration")]
    public class ConfigurationApiController : ApiController, IConfigurationApi
    {
        private IConfigurationService _configurationService;
       
        /// <summary>
        /// default constructor.
        /// </summary>
        /// <param name="configurationService">the configuration service.</param>
        public ConfigurationApiController(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }
        
        /// <summary>
        /// Get all entries in the configuration.
        /// </summary>
        /// <returns>List of key value pairs.</returns>
        [HttpGet]
        [Route("Get")]
        [ResponseType(typeof(List<IConfigItem>))]
        public HttpResponseMessage Get()
        {
            var configEntries = _configurationService.Get();
            return Request.CreateResponse(HttpStatusCode.OK, configEntries);            
        }               

        /// <summary>
        /// Get specific entry from the configuration.
        /// </summary>
        /// <param name="key">Configuration key to search for value.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Get/{key}")]
        [ResponseType(typeof(IConfigItem))]
        public HttpResponseMessage Get(string key)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _configurationService.Get(key));
        }

        /// <summary>
        /// Used to add an entry to the configuration.
        /// </summary>
        /// <param name="configItem">Config item to add.</param>
        /// <returns>true if successful.</returns>
        [HttpPost]
        [Route("Add")]
        [ResponseType(typeof(bool))]
        public HttpResponseMessage Add([ModelBinder(typeof(CustomModelBinder))] IConfigItem configItem)
        {
            var test = configItem;

            return Request.CreateResponse(HttpStatusCode.OK, _configurationService.Add(configItem));
        }

        [HttpPost]
        [Route("AddCustom")]
        [ResponseType(typeof(bool))]
        public HttpResponseMessage AddCustom([FromBody]CustomConfigItem configItem)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _configurationService.Add(configItem));
        }

        /// <summary>
        /// Used to update an entry in the configuration.
        /// </summary>
        /// <param name="configItem">Config item to update.</param>
        /// <returns>true if successful.</returns>
        [HttpPut]
        [Route("Update")]
        [ResponseType(typeof(bool))]
        public HttpResponseMessage Update([FromBody]ConfigItem configItem)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _configurationService.Update(configItem));
        }

        /// <summary>
        /// Used to delete an entry from the configuration.
        /// </summary>
        /// <param name="key">item key to Delete.</param>
        /// <returns></returns>

        [HttpDelete]
        [Route("Delete/{key}")]
        [ResponseType(typeof(bool))]
        public HttpResponseMessage Delete(string key)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _configurationService.Delete(key));
        }
    }
}
