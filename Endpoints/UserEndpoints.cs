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

            app.MapGet("/Users/{UserId}", (UserDbContext context, int UserId) =>
            {
                var userList = context.user.ToList();
                var specificUser = userList.FirstOrDefault(i => i.UserId == UserId);
                return specificUser is not null ? Results.Ok(specificUser) : Results.NotFound($"Item with ID {UserId} not found.");
            });

            app.MapPost("/Users", (User user, UserDbContext context) =>
            {
                user.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password, 13);
                context.Add(user);
                context.SaveChanges();
                return Results.Created();
            });

            //app.MapPut("/Users", (User user, UserDbContext context) =>
            //{

            //});

            app.MapDelete("/Users/{UserId}", (UserDbContext context, int UserId) =>
            {
                return context.user.Where(c => c.UserId == UserId).ExecuteDelete() > 0 ? Results.Ok($"User with ID {UserId} was successfully deleted") : Results.NotFound("User ID specific does not exist");

            });

        }

    }
}
