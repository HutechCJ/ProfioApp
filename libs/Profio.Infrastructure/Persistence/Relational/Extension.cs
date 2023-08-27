using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Profio.Infrastructure.Persistence.Relational;

public static class Extension
{
  public static void AddPostgres(this IServiceCollection services, IConfiguration configuration)
  {

    services.AddDbContextPool<DbContext, ApplicationDbContext>(options =>
      options.UseNpgsql(configuration.GetConnectionString("Postgres"), sqlOptions =>
        sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null))
    );

    services.AddScoped<ApplicationDbContextInitializer>();

    services.AddScoped<IDatabaseFacade>(p => p.GetRequiredService<ApplicationDbContext>());


  }
}
