using Microsoft.AspNetCore.Identity;
using Project.Models.Users;

namespace Project.Services.Security
{
    public class SecurityService : ISecurityService
    {
        public User UserPasswordHashing(User user)
        {
            var PasswordHasher = new PasswordHasher<User>();
            user.Password = PasswordHasher.HashPassword(user, user.Password);

            return user;
        }
    }
}
