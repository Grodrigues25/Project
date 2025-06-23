using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Project.Models;
using Project.Services.Repository;
using Project.Services;

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

                if (userId < 0)
                {
                    return Results.BadRequest("User ID need to be a positive integer");
                }
                var user = await userRepo.GetByIdAsync(userId);
                return user != null ? Results.Ok(user) : Results.NotFound($"There is no product with ID {userId}.");
            }).RequireAuthorization("adminAccess");

            app.MapPost("/Users", async (IRepository<User> userRepo, User user) =>
            {
                var PasswordHasher = new PasswordHasher<User>();
                user.Password = PasswordHasher.HashPassword(user, user.Password);
                await userRepo.AddAsync(user);

                return Results.Created($"/Users/{user.UserId}", user);

            });

            //app.MapPut("/Users", (User user, UserDbContext context) =>
            //{

            //});

            app.MapDelete("/Users/{UserId}", async (IRepository<User> userRepo, int userId, IAuthenticationService auth, HttpRequest request) =>
            {

                bool tokenIsValid = await auth.ValidateJwtToken(request);
                if (!tokenIsValid) return Results.Unauthorized();

                User user = await userRepo.GetByIdAsync(userId);
                await userRepo.DeleteAsync(user);
                return Results.NoContent();

            }).RequireAuthorization("adminAccess");

        }

    }
}
