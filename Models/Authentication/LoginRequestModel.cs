using System.ComponentModel.DataAnnotations;

namespace Project.Models.Authentication
{
    public class LoginRequestModel
    {
        [Required(ErrorMessage = "Email is required"), EmailAddress(ErrorMessage = "Email address format is invalid.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required"), MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }
    }
}
