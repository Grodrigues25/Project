using Project.Services;
using Project.Models.Authentication;

namespace Project.Endpoints
{
    public static class AuthenticationEndpoint
    {
        public static void RegisterAuthenticationEndpoints(this WebApplication app)
        {
            // TODO: Make this Authentication method in Repository Pattern style and see if it resolves the error message, aka create Interface and then use interface in the endpoint.
            app.MapPost("/auth/login", async (IAuthenticationService authService, LoginRequestModel request) =>
            {
                var result = await authService.Authenticate(request);
                if (result == null)
                {
                    return Results.Unauthorized();
                }

                return Results.Ok(result);
            });
        }
    }
}
