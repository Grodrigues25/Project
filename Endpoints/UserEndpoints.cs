using Project.Models.Security;
using Project.Models.Users;
using Project.Services.Authentication;
using Project.Services.Repository;

namespace Project.Endpoints
{
    // https://www.tessferrandez.com/blog/2023/10/31/organizing-minimal-apis.html
    // https://www.youtube.com/watch?v=Wiy54682d1w

    public static class UserEndpoints
    {

        public static void RegisterUserEndpoints(this WebApplication app)
        {
            app.MapGet("/Users", async (IRepository<User> userRepo, IAuthenticationService auth, HttpRequest request) =>
            {
                bool tokenIsValid = await auth.ValidateJwtToken(request);
                if (!tokenIsValid) return Results.Unauthorized();

                var user = await userRepo.GetAsync();
                return Results.Ok(user);

            }).RequireAuthorization("adminAccess");

            app.MapGet("/Users/{UserId}", async (IRepository<User> userRepo, int userId, IAuthenticationService auth, HttpRequest request) =>
            {
                bool tokenIsValid = await auth.ValidateJwtToken(request);
                if (!tokenIsValid) return Results.Unauthorized();

                // constraint so each standard user can only access their own data
                var userIdInToken = auth.GetUserIdFromJwtToken(request.HttpContext);
                var userRoleInToken = auth.GetRoleFromJwtToken(request.HttpContext);
                if (userRoleInToken != "admin" || userIdInToken != userId) return Results.Unauthorized();

                if (userId < 0) return Results.BadRequest("User ID need to be a positive integer");

                var user = await userRepo.GetByIdAsync(userId);
                return user != null ? Results.Ok(user) : Results.NotFound($"There is no product with ID {userId}.");

            });

            app.MapPost("/Users", async (IRepository<User> userRepo, ISecurityService security, User user) =>
            {
                user = security.UserPasswordHashing(user);
                await userRepo.AddAsync(user);
                return Results.Created($"/Users/{user.UserId}", user);

            });

            app.MapPut("/Users/{UserId}", async (User user, int userId, IAuthenticationService auth, ISecurityService security, IRepository<User> userRepo, HttpRequest request) =>
            {
                bool tokenIsValid = await auth.ValidateJwtToken(request);
                if (!tokenIsValid) return Results.Unauthorized();

                if (userId < 0) return Results.BadRequest("User ID need to be a positive integer");
                if (user.UserId != userId) return Results.BadRequest("User ID in the body does not match the User ID in the URL.");

                user = security.UserPasswordHashing(user);  

                int resultCode = await userRepo.UpdateAsync(user);
                if (resultCode > 0) return Results.NoContent();
                else return Results.NotFound($"There is no user with ID {userId}.");

            }).RequireAuthorization("adminAccess");

            app.MapDelete("/Users/{UserId}", async (IRepository<User> userRepo, int userId, IAuthenticationService auth, HttpRequest request) =>
            {
                bool tokenIsValid = await auth.ValidateJwtToken(request);
                if (!tokenIsValid) return Results.Unauthorized();

                if (userId < 0) return Results.BadRequest("User ID need to be a positive integer");

                User user = await userRepo.GetByIdAsync(userId);
                await userRepo.DeleteAsync(user);
                return Results.NoContent();

            }).RequireAuthorization("adminAccess");

        }

    }
}
