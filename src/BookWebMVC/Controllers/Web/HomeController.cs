using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookWebMVC.Data.Core;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;


namespace BookWebMVC.Controllers.Web
{
    public class HomeController : Controller
    {
        private readonly BookWebContext _context;

        public HomeController(BookWebContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
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

        [Authorize]
        public IActionResult UserProfile()
        {
            return View(_context.Users.ToList());
        }
    }
}
