using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Project.Models.Authentication
{
    [PrimaryKey(nameof(Token))]
    public class BlacklistModel
    {
        [Required(ErrorMessage = "Token is required")]
        required public string Token { get; set; }
    }
}
