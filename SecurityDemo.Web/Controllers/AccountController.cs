using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;

namespace SecurityDemo.Web.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Logon()
        {
            return View();
        }

        readonly string[] mockPermissions = new[] { "CanViewDemoIndex", "CanPushTheButton", "CanBeAwesome" };

        [HttpPost]
        public ActionResult Logon(string username, string password, bool isAwesome)
        {
            //do some db lookup to confirm credentials
            var mockPermissions = this.mockPermissions.ToList();
            if (!isAwesome)
                mockPermissions.Remove("CanBeAwesome");

            var ticket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddDays(1), true,
                JsonConvert.SerializeObject(mockPermissions));

            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName);
            authCookie.Value = FormsAuthentication.Encrypt(ticket);
            authCookie.Expires = ticket.Expiration;

            Response.Cookies.Add(authCookie);
            return RedirectToAction("Index", "Demo");
        }

        [HttpGet]
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Logon");   
        }
    }
}