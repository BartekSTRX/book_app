using System;
using System.ComponentModel.DataAnnotations;

namespace BookWebMVC.Data.Model
{
    public class Picture
    {
        public int Id { get; set; }

        [Required]
        public string Path { get; set; }

        public DateTime Uploaded { get; set; }

        public string UploaderId { get; set; }
        public virtual BookWebUser Uploader { get; set; } 
    }
}