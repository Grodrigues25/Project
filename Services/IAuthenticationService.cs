using Microsoft.AspNetCore.Identity;
using Project.Models;
using Project.Models.Authentication;

namespace Project.Services
{
    public interface IAuthenticationService
    {
        Task<LoginResponseModel> Authenticate(LoginRequestModel request);
    }
}
