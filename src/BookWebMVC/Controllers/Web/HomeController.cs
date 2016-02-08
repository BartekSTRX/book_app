using Microsoft.AspNet.Mvc;


namespace BookWebMVC.Controllers.Web
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Nav = "Index";
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Nav = "About";
            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Nav = "Contact";
            return View();
        }
    }
}
