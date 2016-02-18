using System;
using System.Collections.Generic;
using BookWebMVC.Data.Model;

namespace BookWebMVC.ViewModels.Author
{
    public class AuthorDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }

        public DateTime? DeathDate { get; set; }
        public string DeathPlace { get; set; }

        public string Description { get; set; }

        public int? ProfilePictureId { get; set; }
        public string ProfilePictureAlt => Name;

        public List<Book> Books { get; set; }
    }
}