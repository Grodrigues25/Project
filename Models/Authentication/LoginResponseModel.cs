using System.ComponentModel.DataAnnotations;

namespace Project.Models.Authentication
{
    public class LoginResponseModel
    {
        [Required(ErrorMessage = "Emails is required")]
        required public string Email { get; set; }

        [Required(ErrorMessage = "Access Token is required")]
        required public string AccessToken { get; set; }

        [Required(ErrorMessage = "Expires In timestamp is required")]
        public int ExpiresIn { get; set; }
    }
}
