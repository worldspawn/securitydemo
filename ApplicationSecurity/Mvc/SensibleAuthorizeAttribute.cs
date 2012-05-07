using System;
using System.Linq;
using System.Web.Mvc;

namespace ApplicationSecurity.Mvc
{
    public interface IPermissionAccessor<T>
    {
        T[] Permissions { get; set; }
    }

    public abstract class SensibleAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new Http403Result();
                return;
            }

            base.HandleUnauthorizedRequest(filterContext);
        }

        protected void SetPermissions<T>(T[] roles)
        {
            Roles = string.Join(",", roles.Select(p => p.ToString()).ToArray());
        }

        protected T[] GetPermissions<T>()
        {
            return Roles.Split(',').Select(p => (T)Enum.Parse(typeof(T), p)).ToArray();
        }
    }
}