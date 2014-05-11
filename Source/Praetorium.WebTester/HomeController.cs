using Praetorium.Contexts;
using System.Web.Mvc;

namespace Praetorium.WebTester
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var context1 = new HttpRequestOrProcessHybridContext();

            var found = context1.Contains("process-key");

            context1["key"] = "value";

            var context2 = new HttpRequestOrProcessHybridContext();

            var value = context2["key"];

            return View();
        }
    }
}