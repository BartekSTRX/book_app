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

        public IActionResult Show(int id, bool clip = false)
        {
            var picture = _context.Pictures.SingleOrDefault(p => p.Id == id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            try
            {
                var source = Image.FromFile(picture.Path) as Bitmap;

                Bitmap target;
                if (clip)
                {
                    var size = Math.Min(source.Height, source.Width);
                    var x = (source.Width - size)/2;
                    var y = (source.Height - size)/2;
                    target = new Bitmap(size, size);

                    using (var graphics = Graphics.FromImage(target))
                    {
                        graphics.DrawImage(source, 0, 0, new RectangleF(x, y, size, size), GraphicsUnit.Pixel);
                    }                    
                }
                else
                {
                    target = source;
                }

                using (var stream = new MemoryStream())
                {
                    target.Save(stream, ImageFormat.Jpeg);
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
