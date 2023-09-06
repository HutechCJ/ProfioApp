using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Profio.Infrastructure.Identity;

public class UserAccessor : IUserAccessor
{
  private readonly IHttpContextAccessor _httpContextAccessor;

  public UserAccessor(IHttpContextAccessor httpContextAccessor)
    => _httpContextAccessor = httpContextAccessor;
  public string Id => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
  public string UserName => _httpContextAccessor.HttpContext?.User.Identity?.Name!;
  public IList<string> Roles => _httpContextAccessor.HttpContext!.User.Claims
      .Where(x => x.Type == ClaimTypes.Role)
      .Select(x => x.Value)
      .ToList();
}
