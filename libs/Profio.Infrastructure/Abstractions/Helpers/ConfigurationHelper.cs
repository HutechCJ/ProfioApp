using Microsoft.Extensions.Configuration;

namespace Profio.Infrastructure.Abstractions.Helpers;

public static class ConfigurationHelper
{
  public static IConfiguration GetConfiguration(string? basePath = default)
  {
    basePath ??= Directory.GetCurrentDirectory();
    var builder = new ConfigurationBuilder()
      .SetBasePath(basePath)
      .AddJsonFile("appsettings.json")
      .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
      .AddEnvironmentVariables();

    return builder.Build();
  }
}
