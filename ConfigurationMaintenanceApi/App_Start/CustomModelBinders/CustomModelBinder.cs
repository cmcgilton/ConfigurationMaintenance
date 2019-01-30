using System;
using System.Activities;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;
using ConfigurationManager;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using IModelBinder = System.Web.Http.ModelBinding.IModelBinder;
using ModelBindingContext = System.Web.Http.ModelBinding.ModelBindingContext;

namespace ConfigurationMaintenanceApi.CustomModelBinders
{
    public class CustomModelBinder : IModelBinder
    {
        private readonly ICustomModelResolver _modelResolver;

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

            /*
             https://stackoverflow.com/questions/12638741/deserialising-json-to-derived-types-in-asp-net-web-api
             https://www.advisory.com/technology/the-advisory-board-engineering-blog/2014/05/using-abstract-classes-as-controller-parameters-in-aspnet-mvc4
            */

            switch (mediaType)
            {
                case "application/json":

                    //var objType = _modelResolver.Resolve(modelType, content);

                    var model = JsonConvert.DeserializeObject(content, modelType, new JsonTypeConverter(modelType));
                    //var model = JsonConvert.DeserializeObject<T>(content, new JsonTypeConverter<T>());

                    bindingContext.Model = model;
                    break;
                case "application/xml":
                    break;
            }



            //// Get the generic type definition
            //MethodInfo method = typeof(Session).GetMethod("Linq",
            //    BindingFlags.Public | BindingFlags.Static);

            //// Build a method with the specific type argument you're interested in
            //method = method.MakeGenericMethod(typeOne);
            //// The "null" is because it's a static method
            //method.Invoke(null, arguments);




            return true;
        }
    }
}
 