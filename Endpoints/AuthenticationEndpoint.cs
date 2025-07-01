using Project.Models.Authentication;
using Project.Services;
using Project.Services.Repository;
using System.Security.Claims;

namespace Project.Endpoints
{
    public static class AuthenticationEndpoint
    {
        public static void RegisterAuthenticationEndpoints(this WebApplication app)
        {
            app.MapPost("/auth/login", async (IAuthenticationService authService, LoginRequestModel request) =>
            {
                var result = await authService.Authenticate(request);
                if (result == null) return Results.Unauthorized();

                return Results.Ok(result);
            });

            app.MapPost("/auth/logout", async (IAuthenticationService authService, IRepository<BlacklistModel> blacklistRepo, BlacklistModel token, HttpContext context) =>
            {
                var userClaims = context.User.Identity as ClaimsIdentity;
                var userRole = userClaims.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;
                if (userRole != "admin" || userRole != "user") return Results.Unauthorized();

                var result = await authService.LogOut(blacklistRepo, token);
                if (!result) return Results.BadRequest("Logout failed.");

                return Results.Ok("Logged out successfully.");
            });
        }
    }
}
