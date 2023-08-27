using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Neo4jClient;
using Profio.Domain.Interfaces;

namespace Profio.Infrastructure.Persistence.Graph;

public static class Extension
{
  public static void AddNeo4J(this IServiceCollection services, IConfiguration configuration)
  {
    services.Configure<Neo4JSetting>(configuration.GetSection("ConnectionStrings:Neo4j"));
    var settings = new Neo4JSetting();
    configuration.GetSection("ConnectionStrings:Neo4j").Bind(settings);

    var neo4JClient = new BoltGraphClient(
      new Uri(settings.Uri ?? string.Empty),
      settings.Username,
      settings.Password
      );

    neo4JClient.ConnectAsync().GetAwaiter().GetResult();
    services.AddSingleton<IBoltGraphClient>(neo4JClient);
    services.AddScoped(typeof(IGraphOfWork<>), typeof(GraphOfWork<>));
  }
}
