using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SecurityDemo.Web.App_Start;

namespace SecurityDemo.Web.Controllers
{
    public class DemoController : Controller
    {
        [SensibleAuthorizeAttribute(Permissions = new[] { ApplicationPermission.CanViewDemoIndex})]
        public ActionResult Index()
        {
            return View();
        }

        [SensibleAuthorizeAttribute(Permissions = new [] {ApplicationPermission.CanBeAwesome})]
        public ActionResult AwesomePage()
        {
            return View();
        }
    }
}