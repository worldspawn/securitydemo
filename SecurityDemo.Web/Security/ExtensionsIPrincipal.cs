using System.Security.Principal;
using SecurityDemo.Data;

namespace SecurityDemo.Web.Security
{
    public static class ExtensionsIPrincipal
    {
        public static bool HasPermission(this IPrincipal principal, Permission permission)
        {
            return principal.IsInRole(permission.ToString());
        }
    }
}