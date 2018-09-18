using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ConfigurationManager;
using System.Web.Http.Description;

namespace ConfigurationMaintenanceApi.Controllers
{
    /// <summary>
    /// Api Controller for the configuration maintenance.
    /// </summary>
    [RoutePrefix("api/v2/Configuration")]
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
        /// <returns>Dictionary of key value pairs.</returns>
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
        /// used to add an entry to the configuration.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        [ResponseType(typeof(bool))]
        public HttpResponseMessage Add([FromBody]ConfigItem configItem)
        {

            return Request.CreateResponse(HttpStatusCode.OK, _configurationService.Add(configItem));
        }

        /// <summary>
        /// used to update an entry in the configuration.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        [ResponseType(typeof(bool))]
        public HttpResponseMessage Update([FromBody]ConfigItem configItem)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _configurationService.Update(configItem));
        }
    }
}
