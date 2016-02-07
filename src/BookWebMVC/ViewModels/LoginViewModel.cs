using System.ComponentModel.DataAnnotations;
using BookWebMVC.Data.Model;

namespace BookWebMVC.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(255, MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public bool Remember { get; set; }
    }
}