using Project.Models.Users;

namespace Project.Models.Security
{
    public interface ISecurityService
    {
        User UserPasswordHashing(User user);
    }
}
