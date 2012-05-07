using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApplicationSecurity;
using SecurityDemo.Data;

namespace SecurityDemo.Web.Views
{
    public abstract class ApplicationWebViewPage : System.Web.Mvc.WebViewPage
    {
        public PermissionConfig<Permission> PermissionConfig { get; set; }
    }
}