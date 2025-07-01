using Project.Models.Authentication;

namespace Project.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<LoginResponseModel> Authenticate(LoginRequestModel request);
        Task<bool> LogOut(HttpRequest request);
        Task<bool> ValidateJwtToken(HttpRequest request);
        int GetUserIdFromJwtToken(HttpContext context);
        string GetRoleFromJwtToken(HttpContext context);
        BlacklistModel GetJwtTokenFromRequest(HttpRequest request);
    }
}
