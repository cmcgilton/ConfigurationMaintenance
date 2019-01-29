using System;
using System.Activities;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;
using ConfigurationManager;
using Newtonsoft.Json;
using IModelBinder = System.Web.Http.ModelBinding.IModelBinder;
using ModelBindingContext = System.Web.Http.ModelBinding.ModelBindingContext;

namespace ConfigurationMaintenanceApi.CustomModelBinders
{
    public class CustomModelBinder : IModelBinder
    {
        private ICustomModelResolver _modelResolver;

        public CustomModelBinder(ICustomModelResolver modelResolver)
        {
            _modelResolver = modelResolver;
        }
        
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var modelType = bindingContext.ModelType;
            var modelName = bindingContext.ModelName;

            var mediaType = actionContext.ControllerContext.Request.Content.Headers.ContentType.MediaType;
            var content = actionContext.Request.Content.ReadAsStringAsync().Result;

            switch (mediaType)
            {
                case "application/json":
                    var model = JsonConvert.DeserializeObject(content);

                    bindingContext.Model = model;
                    break;
                case "application/xml":
                    break;
            }
            

            
            
            

            

            return true;
        }
    }
}