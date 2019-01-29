using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;

namespace ConfigurationMaintenanceApi.CustomModelBinders
{
    public class JsonTypeConverter<T> : JsonCreationConverter<T>
    {
        protected override T Create(Type objectType, JObject jObject)
        {
            throw new NotImplementedException();
        }
    }
}