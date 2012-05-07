using ApplicationSecurity.Mvc;
using SecurityDemo.Data;

namespace SecurityDemo.Web.Security
{
    public class PermissionAuthorizeAttribute : SensibleAuthorizeAttribute, IPermissionAccessor<Permission>
    {
        public Permission[] Permissions
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }
    }
}
