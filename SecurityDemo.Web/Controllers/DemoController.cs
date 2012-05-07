using System.Web.Mvc;
using ApplicationSecurity.Mvc;
using SecurityDemo.Data;
using SecurityDemo.Web.Security;

namespace SecurityDemo.Web.Controllers
{
    public class DemoController : Controller
    {
        [PermissionAuthorize(Permissions = new[] { Permission.ManageCar })]
        public ActionResult Index()
        {
            return View();
        }

        [PermissionAuthorize(Permissions = new[] { Permission.ManageCar })]
        public ActionResult AwesomePage()
        {
            return View();
        }
    }
}