using System.Collections.Generic;
using System.Security.AccessControl;

namespace BookWebMVC.Data.Model
{
    public class Book
    {
        public Book()
        {
            Authors = new List<Author>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public List<Author> Authors { get; set; }
        public int YearPublished { get; set; }
        public string Descrption { get; set; }
        public Genre Genre { get; set; }

        public int CoverPictureId { get; set; }
        public virtual Picture CoverPicture { get; set; } 
    }
}