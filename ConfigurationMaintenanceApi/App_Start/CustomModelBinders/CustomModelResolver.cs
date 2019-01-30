using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConfigurationManager;

namespace ConfigurationMaintenanceApi.CustomModelBinders
{
    public class CustomModelResolver : ICustomModelResolver
    {
        public Type Resolve(object baseType, string jsonObject)
        {


            return typeof(ConfigItem);
        }
    }
}