using Microsoft.EntityFrameworkCore;

namespace Project.Models.Authentication
{
    [PrimaryKey(nameof(Token))]

    public class BlacklistModel
    {
        public required string Token { get; set; }
    }
}
