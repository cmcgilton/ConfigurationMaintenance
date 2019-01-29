using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using ConfigurationMaintenanceApi.CustomModelBinders;
using Newtonsoft.Json;

namespace ConfigurationMaintenanceApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Services.Insert(typeof(ModelBinderProvider), 0, new CustomModelBinderProvider());
            //config.Formatters.JsonFormatter.SerializerSettings.TypeNameHandling = TypeNameHandling.All;

            // Web API routes
            config.MapHttpAttributeRoutes();

            GlobalConfiguration.Configuration.Filters.Add(new GlobalExceptionHandlerAttribute());
        }
    }
}
