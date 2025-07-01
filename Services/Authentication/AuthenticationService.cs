using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project.Models.Authentication;
using Project.Services.Database;
using Project.Services.Repository;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{

    private readonly UserDbContext _dbcontext;
    private readonly IConfiguration _configuration;
    private readonly IRepository<BlacklistModel> _blacklistRepo;

    public AuthenticationService(UserDbContext dbContext, IConfiguration configuration, IRepository<BlacklistModel> blacklistRepo)
    {
        _dbcontext = dbContext;
        _configuration = configuration;
        _blacklistRepo = blacklistRepo;
    }

    public async Task<LoginResponseModel> Authenticate(LoginRequestModel request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
        {
            return null;
        }

        var PasswordHasher = new PasswordHasher<LoginRequestModel>();
        var userAccount = await _dbcontext.user.Where(u => u.Email == request.Email).FirstOrDefaultAsync();
        if (userAccount == null || PasswordHasher.VerifyHashedPassword(request, userAccount.Password, request.Password)== PasswordVerificationResult.Failed) {
            return null;
        }

        var issuer = _configuration["JwtConfig:Issuer"];
        var audience = _configuration["JwtConfig:Audience"];
        var key = _configuration["JwtConfig:Key"];
        var tokenValidityMins = _configuration.GetValue<int>("JwtConfig:TokenValidityMins");
        var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Name, request.Email),
            new Claim(ClaimTypes.Role, userAccount.IsAdmin ? "admin" : "user"),
            new Claim("id", userAccount.UserId.ToString()),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = tokenExpiryTimeStamp,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(securityToken);

        return new LoginResponseModel
        {
            AccessToken = accessToken,
            Email = request.Email,
            ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds,
        };
    }

    public async Task<bool> LogOut(HttpRequest request)
    {
        var token = GetJwtTokenFromRequest(request);
        await _blacklistRepo.AddAsync(token);
        return true;
    }

    public async Task<bool> ValidateJwtToken(HttpRequest request)
    {
        var token = GetJwtTokenFromRequest(request);
        var tokenBlacklisted = await _dbcontext.blacklist.Where(u => u.Token == token.Token).FirstOrDefaultAsync();

        if (tokenBlacklisted != null)
        {
            return false; // Token is blacklisted
        }

        return true; // Token is not blacklisted
    }

    public string GetRoleFromJwtToken(HttpContext context)
    {
        var userClaims = context.User.Identity as ClaimsIdentity;
        var userRole = userClaims.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;

        return userRole;
    }

    public int GetUserIdFromJwtToken(HttpContext context)
    {
        var userClaims = context.User.Identity as ClaimsIdentity;
        var userIdInToken = int.Parse(userClaims.FindFirst("id").Value);

        return userIdInToken;
    }

    public BlacklistModel GetJwtTokenFromRequest (HttpRequest request)
    {
        var requestHeaders = request.Headers["Authorization"].ToString();
        var tokenHeadersSplit = requestHeaders.Split(' ');
        var tokenString = tokenHeadersSplit.Length > 1 ? tokenHeadersSplit[1] : string.Empty;

        BlacklistModel token = new BlacklistModel
        {
            Token = tokenString,
        };

        return token;
    }
}
