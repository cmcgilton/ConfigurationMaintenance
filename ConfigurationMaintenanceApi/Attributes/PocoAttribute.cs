using System;

namespace ConfigurationMaintenanceApi
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class PocoAttribute : Attribute
    {
        public PocoAttribute(Type type)
        {
            PocoType = type;
        }

        public Type PocoType { get; set; }
    }
}