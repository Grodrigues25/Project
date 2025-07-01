using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Project.Models.Authentication
{
    [PrimaryKey(nameof(Token))]
    public class BlacklistModel
    {
        [Required(ErrorMessage = "Token is required")]
        public required string Token { get; set; }
    }
}
