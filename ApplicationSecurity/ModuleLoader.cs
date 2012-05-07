using System.Web;
using System.Web.Mvc;

namespace ApplicationSecurity
{
    public class ModuleLoader<T> : IHttpModule where T : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            var module = DependencyResolver.Current.GetService<T>();
            module.Init(context);
        }

        public void Dispose()
        {
        }
    }
}