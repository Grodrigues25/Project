using Project.Services;
using Project.Models.Authentication;
using Project.Services.Repository;

namespace Project.Endpoints
{
    public static class AuthenticationEndpoint
    {
        public static void RegisterAuthenticationEndpoints(this WebApplication app)
        {
            app.MapPost("/auth/login", async (IAuthenticationService authService, LoginRequestModel request) =>
            {
                var result = await authService.Authenticate(request);
                if (result == null)
                {
                    return Results.Unauthorized();
                }

                return Results.Ok(result);
            });

            app.MapPost("/auth/logout", async (IAuthenticationService authService, IRepository<BlacklistModel> blacklistRepo, BlacklistModel token) =>
            {
                var result = await authService.LogOut(blacklistRepo, token);
                if (!result)
                {
                    return Results.BadRequest("Logout failed.");
                }
                return Results.Ok("Logged out successfully.");
            });
        }
    }
}
