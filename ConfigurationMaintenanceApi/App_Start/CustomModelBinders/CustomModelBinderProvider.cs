using System;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace ConfigurationMaintenanceApi.CustomModelBinders
{
    public class CustomModelBinderProvider : ModelBinderProvider
    {
        public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
        {
            return new CustomModelBinder(new CustomModelResolver());
        }
    }
}