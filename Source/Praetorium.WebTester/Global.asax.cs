using Praetorium.Contexts;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Praetorium.WebTester
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);

            var context = new HttpRequestOrProcessHybridContext();

            context.Add("process-key", "value");


            var context2 = new HttpRequestOrProcessHybridContext();

            var found = context2.Contains("process-key");
        }

        private void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
   }
}