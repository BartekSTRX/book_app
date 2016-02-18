using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookWebMVC.Data.Core;
using BookWebMVC.ViewModels.Author;
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

        public IActionResult Details(int id)
        {
            var model = _context.Authors.SingleOrDefault(a => a.Id == id);
            if (model != null)
            {
                var vm = new AuthorDetailsViewModel
                {
                    Id = model.Id,
                    Name = $"{model.FirstName} {model.LastName}",
                    BirthDate = model.BirthDate,
                    BirthPlace = model.BirthPlace,
                    DeathDate = model.DeathDate,
                    DeathPlace = model.DeathPlace,
                    Description = model.Description,
                    ProfilePictureId = model.ProfilePictureId
                };

                var books = _context.Books
                    .Where(b => b.Authors.Any(a => a.Id == id))
                    .ToList();
                vm.Books = books;

                return View(vm);
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}
