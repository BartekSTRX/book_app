using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookWebMVC.Data.Core;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;


namespace BookWebMVC.Controllers.Web
{
    public class UserController : Controller
    {
        private readonly BookWebContext _context;

        public UserController(BookWebContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Profile()
        {
            //TODO
            return View();
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            // TODO
            return View();
        }
    }
}
