using System;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace ConfigurationMaintenanceApi.CustomModelBinders
{
    public class ConfigModelBinderProvider : ModelBinderProvider
    {
        public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
        {
            return new ConfigModelBinder(new ConfigModelResolver());
        }
    }
}