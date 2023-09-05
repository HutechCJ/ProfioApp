namespace Profio.Infrastructure.Identity;
public interface ITokenService
{
  string CreateToken(ApplicationUser user);
  DateTime GetExpireDate(string? token);
  bool ValidateToken(string? token);
}
