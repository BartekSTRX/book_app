using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookWebMVC.Data.Model
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(3)]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string BirthPlace { get; set; }

        public DateTime? DeathDate { get; set; }
        public string DeathPlace { get; set; }

        public string Description { get; set; }

        public int? ProfilePictureId { get; set; }
        public virtual Picture ProfilePicture { get; set; }
    }
}
