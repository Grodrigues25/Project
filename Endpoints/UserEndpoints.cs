using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Services;
using Project.Services.UserManagementService;

namespace Project.Endpoints
{
    // https://www.tessferrandez.com/blog/2023/10/31/organizing-minimal-apis.html
    // https://www.youtube.com/watch?v=Wiy54682d1w

    public static class UserEndpoints
    {

        public static void RegisterUserEndpoints(this WebApplication app)
        {
            app.MapGet("/Users", async (IRepository<User> userRepo) =>
            {
                return await userRepo.GetAsync(); 
            });

            app.MapGet("/Users/{UserId}", async (IRepository<User> userRepo, int userId) =>
            {
                return await userRepo.GetByIdAsync(userId);
            });

            app.MapPost("/Users", async (IRepository<User> userRepo, User user) =>
            {
                user.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password, 13);
                return await userRepo.AddAsync(user);

            });

            //app.MapPut("/Users", (User user, UserDbContext context) =>
            //{

            //});

            app.MapDelete("/Users/{UserId}", async (IRepository<User> userRepo, int userId) =>
            {
                User user = await userRepo.GetByIdAsync(userId);
                return await userRepo.DeleteAsync(user);

            });

        }

    }
}
