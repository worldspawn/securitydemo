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
        [SensibleAuthorizeAttribute(Roles = "CanViewDemoIndex")]
        public ActionResult Index()
        {
            return View();
        }

        [SensibleAuthorizeAttribute(Roles = "CanBeAwesome")]
        public ActionResult AwesomePage()
        {
            return View();
        }
    }
}