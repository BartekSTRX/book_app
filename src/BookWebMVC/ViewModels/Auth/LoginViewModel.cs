using System.ComponentModel.DataAnnotations;

namespace BookWebMVC.ViewModels.Auth
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