using ApplicationSecurity.Mvc;
using SecurityDemo.Data;

namespace SecurityDemo.Web.Security
{
    public class PermissionAuthorizeAttribute : SensibleAuthorizeAttribute, IPermissionAccessor<Permission>
    {
        public Permission[] Permissions
        {
            get { return GetPermissions<Permission>(); }
            set { SetPermissions(value); }
        }
    }
}