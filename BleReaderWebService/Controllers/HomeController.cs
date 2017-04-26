using System.Web.Mvc;

namespace BleReaderWebService.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Chat()
        {
            return View();
        }
    }
}
