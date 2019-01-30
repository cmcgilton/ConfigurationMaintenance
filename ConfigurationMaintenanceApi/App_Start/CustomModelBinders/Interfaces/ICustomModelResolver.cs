using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationMaintenanceApi.CustomModelBinders
{
    public interface ICustomModelResolver
    {
        Type Resolve(object baseType, string jsonObject);
    }
}
