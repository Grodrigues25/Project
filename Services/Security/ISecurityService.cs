using Project.Models.Users;

namespace Project.Services.Security
{
    public interface ISecurityService
    {
        User UserPasswordHashing(User user);
    }
}
