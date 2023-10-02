using EntityFramework.Exceptions.PostgreSQL;
using EntityFrameworkCore.UnitOfWork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using Profio.Domain.Exceptions;
using Profio.Infrastructure.Persistence.Interceptors;
using Profio.Infrastructure.Persistence.Optimization;

namespace Profio.Infrastructure.Persistence;

public static class Extension
{
  public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddTriggeredDbContextPool<DbContext, ApplicationDbContext>(options =>
      options.UseNpgsql(configuration.GetConnectionString("Postgres"),
          sqlOptions =>
          {
            sqlOptions.MigrationsAssembly(AssemblyReference.AssemblyName);
            sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
          })
        .EnableDetailedErrors()
        .EnableSensitiveDataLogging()
        .UseExceptionProcessor()
        .UseModel(ApplicationDbContextModel.Instance)
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
        .UseTriggers(o => o.AddAssemblyTriggers())
        .AddInterceptors(new TimingInterceptor())
        .AddInterceptors(new ExecuteWithoutWhereCommandInterceptor())
    );

    services.AddScoped<ApplicationDbContextInitializer>();

    services.AddScoped<IDatabaseFacade>(p => p.GetRequiredService<ApplicationDbContext>());

    services.AddUnitOfWork();
    services.AddUnitOfWork<ApplicationDbContext>();

    return services;
  }

  public static void MigrateDataFromScript(this MigrationBuilder migrationBuilder)
  {
    var assembly = AssemblyReference.CallingAssembly;
    var files = assembly.GetManifestResourceNames();
    var filePrefix = $"{assembly.GetName().Name}.Data.Scripts.";

    if (!files.All(f => f.StartsWith(filePrefix) && f.EndsWith(".sql")))
      return;

    foreach (var file in files
               .Where(f => f.StartsWith(filePrefix) && f.EndsWith(".sql"))
               .Select(f => new { PhysicalFile = f, LogicalFile = f.Replace(filePrefix, string.Empty) })
               .OrderBy(f => f.LogicalFile))
    {
      using var stream = assembly.GetManifestResourceStream(file.PhysicalFile);
      using var reader = new StreamReader(stream!);
      var command = reader.ReadToEnd();

      if (string.IsNullOrWhiteSpace(command))
        continue;

      migrationBuilder.Sql(command);
    }
  }

  public static async Task DoDbMigrationAsync(this IApplicationBuilder app, ILogger logger)
  {
    var scope = app.ApplicationServices.CreateAsyncScope();
    var dbFacadeResolver = scope.ServiceProvider.GetService<IDatabaseFacade>();
    var policy = CreatePolicy(3, logger, nameof(WebApplication));

    await policy.ExecuteAsync(async () =>
    {
      if (!await dbFacadeResolver?.Database.CanConnectAsync()!)
      {
        logger.LogError("Connection String: {conn}",
          dbFacadeResolver.Database.GetConnectionString());
        throw new ConnectDatabaseException();
      }

      var migrations = await dbFacadeResolver.Database.GetPendingMigrationsAsync();
      if (migrations.Any())
      {
        await dbFacadeResolver.Database.MigrateAsync();
        logger.LogInformation("Migrated is done");
      }
    });
    return;

    static AsyncRetryPolicy CreatePolicy(int retries, ILogger logger, string prefix)
      => Policy.Handle<Exception>().WaitAndRetryAsync(
        retries,
        _ => TimeSpan.FromSeconds(15),
        (exception, _, retry, _) =>
          logger.LogWarning(exception,
            "[{Prefix}] Exception {ExceptionType} with message {Message} detected on attempt {Retry} of {Retries}",
            prefix, exception.GetType().Name, exception.Message, retry, retries)
      );
  }
}
