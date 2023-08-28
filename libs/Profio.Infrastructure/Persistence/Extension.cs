using EntityFrameworkCore.UnitOfWork.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Profio.Infrastructure.Persistence.Relational.Optimization;

namespace Profio.Infrastructure.Persistence;

public static class Extension
{
  public static void AddPostgres(this IServiceCollection services, IConfiguration configuration)
  {

    services.AddDbContextPool<DbContext, ApplicationDbContext>(options =>
    {
      options.UseNpgsql(configuration.GetConnectionString("Postgres"),
        sqlOptions =>
        {
          sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
          sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
        })
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors();
      options.UseModel(ApplicationDbContextModel.Instance);
    });

    services.AddScoped<ApplicationDbContextInitializer>();

    services.AddScoped<IDatabaseFacade>(p => p.GetRequiredService<ApplicationDbContext>());

    services.AddUnitOfWork();
    services.AddUnitOfWork<ApplicationDbContext>();
  }
}
