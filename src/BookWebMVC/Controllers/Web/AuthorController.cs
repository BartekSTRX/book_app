using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookWebMVC.Data.Core;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;


namespace BookWebMVC.Controllers.Web
{
    public class AuthorController : Controller
    {
        private readonly BookWebContext _context;

        public AuthorController(BookWebContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = _context.Authors.Include(a => a.ProfilePicture).ToList();
            return View(model);
        }
    }
}
