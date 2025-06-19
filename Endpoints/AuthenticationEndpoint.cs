using Microsoft.AspNetCore.Identity.Data;
using Project.Services;
using Project.Models.Authentication;

namespace Project.Endpoints
{
    public static class AuthenticationEndpoint
    {
        public static void RegisterAuthenticationEndpoints(this WebApplication app)
        {
            // Make this Authentication method in Repository Pattern style and see if it resolves the error message.
            app.MapPost("/auth/login", async (AuthenticationService authService, LoginRequestModel request) =>
            {
                var result = await authService.Authenticate(request);
                if (result == null)
                {
                    return Results.Unauthorized();
                }

                return result;
            });
        }
    }
}
