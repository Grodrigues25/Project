using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Project.Models;
using Project.Services;

// Implement Service Principal for the application so the Application runs against the Azure SQL Database with the Service Principal credentials. JWT maybe?

var builder = WebApplication.CreateBuilder();

builder.Services.AddOpenApi();

var connection = String.Empty;
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
    connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
}
else
{
    connection = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
}

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(connection));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.MapGet("/", () => "Hello world!");

app.MapGet("/Users", (UserDbContext context) =>
{
    return context.user.ToList();
});

app.MapPost("/Users", (User user, UserDbContext context) =>
{
    user.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password, 13);
    context.Add(user);
    context.SaveChanges();
});

//app.MapGet("/Authenticate", (User user, UserDbContext context) =>
//{

//});

app.Run();

