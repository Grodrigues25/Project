using Project.Models.Authentication;
using Project.Services.Repository;

namespace Project.Services
{
    public interface IAuthenticationService
    {
        Task<LoginResponseModel> Authenticate(LoginRequestModel request);
        Task<bool> LogOut(IRepository<BlacklistModel> blacklistRepo, BlacklistModel token);
        Task<bool> ValidateJwtToken(HttpRequest request);
    }
}
