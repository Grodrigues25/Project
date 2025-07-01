using Project.Models.Authentication;
using Project.Services.Authentication;
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

            app.MapPost("/auth/logout", async (IAuthenticationService authService, HttpRequest request) =>
            {
                var userRole = authService.GetRoleFromJwtToken(request.HttpContext);
                if (userRole != "admin" && userRole != "user") return Results.BadRequest("You need to be loged in to log out.");

                var result = await authService.LogOut(request);
                if (!result) return Results.BadRequest("Logout failed.");

                return Results.Ok("Logged out successfully.");
            });
        }
    }
}
