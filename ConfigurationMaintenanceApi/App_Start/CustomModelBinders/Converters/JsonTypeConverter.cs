using System;
using Newtonsoft.Json.Linq;
using ConfigurationManager;

namespace ConfigurationMaintenanceApi.CustomModelBinders
{
    public class JsonTypeConverter : JsonCreationConverter<T> where T : new ()
    {
        public JsonTypeConverter(Type t)
        {
        }

        protected override T Create(Type objectType, JObject jObject)
        {
            if ("DerivedType".Equals(jObject.Value<string>("type")))
            {
            }

            return new T();
        }
    }
}