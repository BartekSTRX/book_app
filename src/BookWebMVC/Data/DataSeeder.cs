using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookWebMVC.Configuration;
using BookWebMVC.Data.Core;
using Microsoft.AspNet.Builder;
using BookWebMVC.Data.Model;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Hosting.Internal;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.OptionsModel;

namespace BookWebMVC.Data
{
    public class DataSeeder
    {
        private readonly UserManager<BookWebUser> _userManager;
        private readonly BookWebContext _context;
        private readonly IOptions<PicturesFolder> _options;

        public DataSeeder(UserManager<BookWebUser> userManager, BookWebContext context, IOptions<PicturesFolder> options)
        {
            _userManager = userManager;
            _context = context;
            _options = options;
        }

        public async Task SeedDataAsync()
        {
            if (_context.Users.Any())
            {
                return;
            }

            var user1 = new BookWebUser
            {
                UserName = "user1",
                Email = "user1@ab.cd"
            };
            var user2 = new BookWebUser
            {
                UserName = "user2",
                Email = "user2@ef.gh"
            };

            if (await _userManager.FindByEmailAsync(user1.Email) == null)
            {
                await _userManager.CreateAsync(user1, "P@ssw0rd!1");
            }
            if (await _userManager.FindByEmailAsync(user2.Email) == null)
            {
                await _userManager.CreateAsync(user2, "P@ssw0rd!2");
            }

            var catPic = new Picture
            {
                Uploaded = DateTime.Now, Uploader = user1,
                UploaderId = user1.Id,
                Path = Path.Combine(_options.Value.PicturesFolderSeed, "Mackiewicz.jpg")
            };
            _context.Add(catPic);
            var terryPic = new Picture
            {
                Uploaded = DateTime.Now, Uploader = user2,
                UploaderId = user2.Id,
                Path = Path.Combine(_options.Value.PicturesFolderSeed, "Pratchett.jpg")
            };
            _context.Add(terryPic);
            _context.SaveChanges();

            var cat = new Author
            {
                FirstName = "Stanisław", LastName = "Mackiewicz",
                BirthDate = new DateTime(1896, 12, 18), BirthPlace = "Petersburg, Russia",
                DeathDate = new DateTime(1966, 02, 18), DeathPlace = "Warsaw, Poland",
                Description = "Also known as 'Cat', brother of Józef Mackiewicz",
                ProfilePictureId = catPic.Id, ProfilePicture = catPic
            };
            _context.Authors.Add(cat);
            var pratchet = new Author
            {
                FirstName = "Terry", LastName = "Pratchett",
                BirthDate = new DateTime(1948, 04, 28), BirthPlace = "Beaconsfield, Great Britain",
                DeathDate = new DateTime(2015, 03, 12), DeathPlace = "Broad Chalke, Great Britain",
                Description = "Disc World series author",
                ProfilePictureId = terryPic.Id, ProfilePicture = terryPic
            };
            _context.Authors.Add(pratchet);
            _context.SaveChanges();

            
        }
    }
}
