using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Profio.Infrastructure.Auth;

public sealed class UserAccessor : IUserAccessor
{
  private readonly IHttpContextAccessor _httpContextAccessor;

  public UserAccessor(IHttpContextAccessor httpContextAccessor)
    => _httpContextAccessor = httpContextAccessor;

  public string Id => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

  public string UserName => _httpContextAccessor.HttpContext?.User.Identity?.Name!;

  public string JwtToken => _httpContextAccessor.HttpContext?.Request.Headers["Authorization"]
    .ToString()
    .Replace("Bearer ", string.Empty) ?? string.Empty;

  public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated == true;

  public IList<string> Roles => _httpContextAccessor.HttpContext!.User.Claims
    .Where(x => x.Type == ClaimTypes.Role)
    .Select(x => x.Value)
    .ToList();
}
