using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Services;

namespace Project.Endpoints
{
    //https://www.tessferrandez.com/blog/2023/10/31/organizing-minimal-apis.html

    public static class UserEndpoints
    {
        public static void RegisterUserEndpoints(this WebApplication app)
        {
            app.MapGet("/Users", (UserDbContext context) =>
            {
                return Results.Ok(context.user.ToList());
            });

            app.MapGet("/Users/{UserId}", async (UserDbContext context, int UserId) =>
            {
                var userList = await context.user.ToListAsync();
                var specificUser = userList.FirstOrDefault(i => i.UserId == UserId);
                return specificUser is not null ? Results.Ok(specificUser) : Results.NotFound($"Item with ID {UserId} not found.");
            });

            app.MapPost("/Users", async (User user, UserDbContext context) =>
            {
                user.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password, 13);
                await context.AddAsync(user);
                await context.SaveChangesAsync();
                return Results.Created();
            });

            //app.MapPut("/Users", (User user, UserDbContext context) =>
            //{

            //});

            app.MapDelete("/Users/{UserId}", async (UserDbContext context, int UserId) =>
            {          
                return await context.user.Where(c => c.UserId == UserId).ExecuteDeleteAsync() > 0 ? Results.Ok($"User with ID {UserId} was successfully deleted") : Results.NotFound("User ID specific does not exist");
            });

        }

    }
}
