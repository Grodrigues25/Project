using Microsoft.EntityFrameworkCore;
using Project.Endpoints;
using Project.Services.UserManagementService;
using Project.Services.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Project.Services.Authentication;
using Project.Services.ShoppingCartService;
using Project.Services.Database;
using Project.Services.Reports;
using Project.Services.Security;

var builder = WebApplication.CreateBuilder();

builder.Services.AddOpenApi();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IAuthenticationService), typeof(AuthenticationService));
builder.Services.AddScoped(typeof(IShoppingCartService), typeof(ShoppingCartService));
builder.Services.AddScoped(typeof(ISecurityService), typeof(SecurityService));
builder.Services.AddScoped(typeof(IReportingService), typeof(ReportingService));
builder.Services.AddScoped(typeof(IDatabaseService), typeof(DatabaseService));

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
{
    options.UseSqlServer(connection,
    sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
        maxRetryCount: 10,
        maxRetryDelay: TimeSpan.FromSeconds(30),
        errorNumbersToAdd: null);
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
        ValidAudience = builder.Configuration["JwtConfig:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
    };
});

builder.Services.AddAuthorization();

builder.Services.AddAuthorizationBuilder()
  .AddPolicy("adminAccess", policy =>
        policy
            .RequireRole("admin"));

builder.Services.AddAuthorizationBuilder()
  .AddPolicy("userAccess", policy =>
        policy
            .RequireRole("user"));

var app = builder.Build();

app.RegisterUserEndpoints();
app.RegisterProductEndpoints();
app.RegisterAuthenticationEndpoints();
app.RegisterOrderEndpoints();
app.RegisterShoppingCartEndpoints();
app.RegisterReportEndpoints();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
