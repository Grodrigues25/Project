using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Azure.Identity;
using Microsoft.Extensions.Azure;
using Project.Models;
using Project.Services;

// https://learn.microsoft.com/en-us/sql/connect/ado-net/sql/azure-active-directory-authentication?view=sql-server-ver17#using-service-principal-authentication
// + Add SP as a Contributor in the SQL Server
// + https://learn.microsoft.com/en-us/azure/azure-sql/database/authentication-aad-service-principal-tutorial?view=azuresql#create-the-service-principal-user

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

