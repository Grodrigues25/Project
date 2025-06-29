using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project.Models.Authentication;
using Project.Services.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project.Services;

public class AuthenticationService : IAuthenticationService
{

    private readonly UserDbContext _dbcontext;
    private readonly IConfiguration _configuration;

    public AuthenticationService(UserDbContext dbContext, IConfiguration configuration)
    {
        _dbcontext = dbContext;
        _configuration = configuration;
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
            new Claim("id", userAccount.UserId.ToString())
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
            ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds
        };

    }

    public async Task<bool> LogOut(IRepository<BlacklistModel> blacklistRepo, BlacklistModel token)
    {
        try
        {
            await blacklistRepo.AddAsync(token);
            return true;
        }
        catch (Exception ex)
        {
            // Log the exception (ex) as needed
            return false;
        }
    }

    public async Task<bool> ValidateJwtToken(HttpRequest request)
    {
        var requestHeaders = request.Headers["Authorization"].ToString();
        var tokenHeadersSplit = requestHeaders.Split(' ');
        var token = tokenHeadersSplit.Length > 1 ? tokenHeadersSplit[1] : string.Empty;

        var tokenBlacklisted = await _dbcontext.blacklist.Where(u => u.Token == token).FirstOrDefaultAsync();
        
        if (tokenBlacklisted != null)
        {
            return false; // Token is blacklisted
        }
        return true; // Token is not blacklisted
    }
}
