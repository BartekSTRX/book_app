using System.ComponentModel.DataAnnotations;

namespace BookWebMVC.ViewModels.Auth
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(255, MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}