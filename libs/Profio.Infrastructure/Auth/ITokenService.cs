using Profio.Domain.Identity;

namespace Profio.Infrastructure.Auth;
public interface ITokenService
{
  string CreateToken(ApplicationUser user);
  DateTime GetExpireDate(string? token);
  bool ValidateToken(string? token);
}
