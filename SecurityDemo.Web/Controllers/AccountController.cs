using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ApplicationSecurity;
using Newtonsoft.Json;
using SecurityDemo.Data;

namespace SecurityDemo.Web.Controllers
{
    public class AccountController : Controller
    {
        public AccountController()
        {
            mockPermissions = (int[])Enum.GetValues(typeof(Permission));
            _config = new PermissionConfig<Permission>(Server.MapPath("~/add_data/permissions.xml"));
        }

        [HttpGet]
        public ActionResult Logon()
        {
            return View();
        }

        readonly int[] mockPermissions;
        private PermissionConfig<Permission> _config;

        [HttpPost]
        public ActionResult Logon(string username, string password, string redirectUrl)
        {
            //do some db lookup to confirm credentials
            var mockPermissions = this.mockPermissions.ToList();

            var ticket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddDays(1), true,
                JsonConvert.SerializeObject(mockPermissions));

            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName);
            authCookie.Value = FormsAuthentication.Encrypt(ticket);
            authCookie.Expires = ticket.Expiration;

            Response.Cookies.Add(authCookie);

            if (string.IsNullOrWhiteSpace(redirectUrl))
                return RedirectToAction("Index", "Demo");

            return Redirect(redirectUrl);
        }

        [HttpGet]
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Logon");   
        }
    }
}