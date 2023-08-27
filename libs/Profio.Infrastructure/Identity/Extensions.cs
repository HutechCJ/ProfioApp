using EntityFrameworkCore.UnitOfWork.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Profio.Infrastructure.Persistence.Relational;

namespace Profio.Infrastructure.Identity;

public static class Extensions
{
  public static void AddIdentity(this IServiceCollection services)
  {
    services.AddIdentityCore<ApplicationUser>(options =>
    {
      options.Password.RequireDigit = true;
      options.Password.RequireLowercase = true;
      options.Password.RequireNonAlphanumeric = true;
      options.Password.RequireUppercase = true;
      options.Password.RequiredLength = 6;
      options.Password.RequiredUniqueChars = 1;

      options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
      options.Lockout.MaxFailedAccessAttempts = 5;
      options.Lockout.AllowedForNewUsers = true;

      options.User.RequireUniqueEmail = true;
    })
      .AddEntityFrameworkStores<ApplicationDbContext>();

    services.AddUnitOfWork();
    services.AddUnitOfWork<ApplicationDbContext>();
  }
}