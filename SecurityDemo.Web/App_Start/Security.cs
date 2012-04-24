using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using System.Web.Security;
using System.Security.Principal;
using System.Web.Mvc;

[assembly: WebActivator.PreApplicationStartMethod(typeof(SecurityDemo.Web.App_Start.Security), "PreStart")]

namespace SecurityDemo.Web.App_Start
{
    public static class Security
    {
        public static void PreStart()
        {
            DynamicModuleUtility.RegisterModule(typeof(SecurityModule));
        }
    }

    public class SecurityModule : IHttpModule
    {
        public void Dispose()
        {
            
        }

        public void Init(HttpApplication context)
        {
            context.AuthorizeRequest += new EventHandler(context_AuthenticateRequest);
        }

        void context_AuthenticateRequest(object sender, EventArgs e)
        {
            var app = sender as HttpApplication;
            if (app == null)
                return;

            var user = app.User;
            if (user == null)
                return;

            if (!user.Identity.IsAuthenticated)
                return;

            var authCookie = app.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
                return;

            var ticket = FormsAuthentication.Decrypt(authCookie.Value);
            var permissions = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(ticket.UserData);

            var identity = new GenericIdentity(ticket.Name);
            var principal = new GenericPrincipal(identity, permissions);

            app.Context.User = principal;
        }
    }

    public class SensibleAuthorizeAttribute : AuthorizeAttribute
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
    }

    public class Http403Result : ActionResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.StatusCode = 403;
        }
    }
}