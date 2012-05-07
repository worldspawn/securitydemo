using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

namespace ApplicationSecurity
{
    public static class Security<T>
    {
        public static void PreStart()
        {
            DynamicModuleUtility.RegisterModule(typeof(ModuleLoader<SecurityModule<T>>));
        }
    }
}
