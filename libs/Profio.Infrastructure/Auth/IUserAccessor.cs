namespace Profio.Infrastructure.Auth;

public interface IUserAccessor
{
  public string Id { get; }
  public IList<string> Roles { get; }
  public string UserName { get; }
  public string JwtToken { get; }
  public bool IsAuthenticated { get; }
}
