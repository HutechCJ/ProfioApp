using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Profio.Infrastructure.Persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Profio.Infrastructure.Identity;

public class TokenService : ITokenService
{
  private readonly TimeSpan _tokenLifespan;
  private readonly SigningCredentials _signingCredentials;
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly RoleManager<IdentityRole> _roleManager;

  public TokenService(IConfiguration configuration, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
  {
    var tokenKey = configuration["Authentication:TokenKey"] ?? string.Empty;
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
    _signingCredentials = new(key, SecurityAlgorithms.HmacSha256Signature);
    _tokenLifespan = TimeSpan.FromHours(5);
    _userManager = userManager;
    _roleManager = roleManager;
  }

  public string CreateToken(ApplicationUser user)
  {
    var tokenClaims = new List<Claim>
    {
      new(ClaimTypes.NameIdentifier, user.Id),
      new(ClaimTypes.GivenName, user.FullName ?? string.Empty),
      new(ClaimTypes.Name, user.UserName ?? string.Empty),
      new(ClaimTypes.Email, user.Email ?? string.Empty)
    };

    tokenClaims.AddRange(GetRoleClaims(user).Result);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new(tokenClaims),
      Expires = DateTime.UtcNow.Add(_tokenLifespan),
      SigningCredentials = _signingCredentials
    };

    var tokenHandler = new JwtSecurityTokenHandler();

    var token = tokenHandler.CreateToken(tokenDescriptor);

    return tokenHandler.WriteToken(token);
  }

  public DateTime GetExpireDate(string? token)
  {
    JwtSecurityToken jwtToken = new(token);
    return token is null
      ? DateTime.UtcNow
      : jwtToken.ValidTo.ToUniversalTime();
  }

  public bool ValidateToken(string? token)
  {
    if (string.IsNullOrEmpty(token))
      return false;

    var handler = new JwtSecurityTokenHandler();

    handler.ValidateToken(token, new()
    {
      ValidateIssuerSigningKey = true,
      IssuerSigningKey = _signingCredentials.Key,
      ValidateIssuer = false,
      ValidateAudience = false,
      ValidateLifetime = true,
      ClockSkew = TimeSpan.FromSeconds(5)
    }, out _);

    return true;
  }

  private async Task<IList<Claim>> GetRoleClaims(ApplicationUser user)
  {
    var roles = await _userManager.GetRolesAsync(user);

    return roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList();
  }
}
