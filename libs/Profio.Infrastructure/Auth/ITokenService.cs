using Profio.Domain.Identity;

namespace Profio.Infrastructure.Auth;

public interface ITokenService
{
  public string CreateToken(ApplicationUser user);
  public DateTime GetExpireDate(string? token);
  public bool ValidateToken(string? token);
}
