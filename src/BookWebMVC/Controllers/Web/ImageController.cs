using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using BookWebMVC.Data.Core;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookWebMVC.Controllers.Web
{
    public class ImageController : Controller
    {
        private readonly BookWebContext _context;

        public ImageController(BookWebContext context)
        {
            _context = context;
        }

        public IActionResult Show(int id)
        {
            var picture = _context.Pictures.SingleOrDefault(p => p.Id == id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            try
            {
                var image = Image.FromFile(picture.Path);

                using (var stream = new MemoryStream())
                {
                    image.Save(stream, ImageFormat.Jpeg);
                    return File(stream.ToArray(), "image/jpg");
                }
            }
            catch (Exception e) when(e is FileNotFoundException || e is ArgumentException)
            {
                return HttpNotFound();
            }
        }
    }
}
