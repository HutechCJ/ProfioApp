using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Neo4j.Driver;

namespace Profio.Infrastructure.Persistence.Neo4j;

public static class Extension
{
  public static void AddNeo4J(this IServiceCollection services, IConfiguration configuration)
  {
    services.Configure<ApplicationSettings>(configuration.GetSection("ConnectionStrings:Neo4j"));
    var settings = new ApplicationSettings();
    configuration.GetSection("ConnectionStrings:Neo4j").Bind(settings);
    services.AddSingleton(
      GraphDatabase.Driver(settings.Uri, AuthTokens.Basic(settings.Username, settings.Password))
      );
  }
}
