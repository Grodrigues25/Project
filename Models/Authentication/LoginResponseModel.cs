using System.ComponentModel.DataAnnotations;

namespace Project.Models.Authentication
{
    public class LoginResponseModel
    {
        [Required(ErrorMessage = "Emails is required")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "Access Token is required")]
        public required string AccessToken { get; set; }
        [Required(ErrorMessage = "Expires In timestamp is required")]
        public int ExpiresIn { get; set; }
    }
}
