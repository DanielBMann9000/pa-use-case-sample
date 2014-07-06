using System.Web.Mvc;
using ASP.NET_MVC_App.Models;

namespace ASP.NET_MVC_App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(bool runSample = false, bool throwException = false)
        {
            if (runSample)
            {
                Sample.RunSample();
            }

            if (throwException)
            {
                Sample.ThrowUnhandledException();
            }

            return View();
        }
    }
}
