using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using SecurityDemo.Web.App_Start;

namespace SecurityDemo.Web.Helper
{
    public static class ExtensionsIPrincipal
    {
        public static bool HasPermission(this IPrincipal principal, ApplicationPermission applicationPermission)
        {
            return principal.IsInRole(applicationPermission.ToString());
        }
    }
}