using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Azure.Identity;
using Microsoft.Extensions.Azure;
using Project.Models;
using Project.Services;
using Project.Endpoints;

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

app.RegisterUserEndpoints();
app.RegisterProductEndpoints();

app.Run();

