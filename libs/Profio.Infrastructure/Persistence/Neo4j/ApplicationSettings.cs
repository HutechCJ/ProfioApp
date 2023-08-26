namespace Profio.Infrastructure.Persistence.Neo4j;

public class ApplicationSettings
{
  public Uri? Uri { get; set; }
  public string? Username { get; set; }
  public string? Password { get; set; }
  public string? InstanceName { get; set; }
}
