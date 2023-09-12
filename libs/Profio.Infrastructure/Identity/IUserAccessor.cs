namespace Profio.Infrastructure.Identity;

public interface IUserAccessor
{
  string Id { get; }
  IList<string> Roles { get; }
  string UserName { get; }
}
