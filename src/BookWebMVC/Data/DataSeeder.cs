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

            var catPic = CreatePicture(user2, "Mackiewicz.jpg");
            _context.Add(catPic);
            var terryPic = CreatePicture(user1, "Pratchett.jpg");
            _context.Add(terryPic);
            var martinPic = CreatePicture(user1, "Martin.jpg");
            _context.Add(martinPic);
            var rothbardPic = CreatePicture(user1, "Rothbard.jpg");
            _context.Add(rothbardPic);
            var sapkowskiPic = CreatePicture(user1, "Sapkowski.jpg");
            _context.Add(sapkowskiPic);
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
            var martin = new Author
            {
                FirstName = "George", LastName = "Matrin",
                BirthDate = new DateTime(1948, 08, 20), BirthPlace = "Bayonne, USA",
                DeathDate = null, DeathPlace = null,
                Description = "A Song of Ice and Fire author, Game of Thrones tv series screenplay writer",
                ProfilePictureId = martinPic.Id, ProfilePicture = martinPic
            };
            _context.Authors.Add(martin);
            var rothbard = new Author
            {
                FirstName = "Murray", LastName = "Rothbard",
                BirthDate = new DateTime(1926, 03, 26), BirthPlace = "New York, USA",
                DeathDate = new DateTime(1995, 01, 07), DeathPlace = null,
                Description = @"Murray Newton Rothbard was an influential American historian, natural law theorist, Aristotelian and economist of the Austrian School who helped define modern libertarianism. Rothbard took the Austrian School's emphasis on spontaneous order and condemnation of central planning to an individualist anarchist conclusion, which he termed ""anarcho - capitalism"".",
                ProfilePictureId = rothbardPic.Id, ProfilePicture = rothbardPic
            };
            _context.Authors.Add(rothbard);
            var sapkowski = new Author
            {
                FirstName = "Andrzej", LastName = "Sapkowski",
                BirthDate = new DateTime(1948, 06, 21), BirthPlace = "Łódź, Poland",
                DeathDate = null, DeathPlace = null,
                Description = "A Polish fantasy writer. Sapkowski studied economics, and before turning to writing, he had worked as a senior sales representative for a foreign trade company. His first short story, The Witcher(Wiedźmin), was published in Fantastyka, Poland's leading fantasy literary magazine, in 1986 and was enormously successful both with readers and critics. Sapkowski has created a cycle of tales based on the world of The Witcher, comprising three collections of short stories and five novels. This cycle and his many other works have made him one of the best-known fantasy authors in Poland in the 1990s.",
                ProfilePictureId = sapkowskiPic.Id, ProfilePicture = sapkowskiPic
            };
            _context.Authors.Add(sapkowski);
            _context.SaveChanges();

            
        }

        private Picture CreatePicture(BookWebUser user, string filename)
        {
            return new Picture
            {
                Uploaded = DateTime.Now,
                Uploader = user,
                UploaderId = user.Id,
                Path = Path.Combine(_options.Value.PicturesFolderSeed, filename)
            };
        }
    }
}
