using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookWebMVC.Data.Core;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;


namespace BookWebMVC.Controllers.Web
{
    public class BookController : Controller
    {
        private readonly BookWebContext _context;

        public BookController(BookWebContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var books = _context.Books.AsNoTracking().ToList();

            return View(books);
        }
    }
}
