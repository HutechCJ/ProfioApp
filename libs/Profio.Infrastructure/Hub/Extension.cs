namespace Profio.Infrastructure.Hub;

public static class Extension
{
  public static void AddSocketHub(this WebApplicationBuilder builder)
    => builder.Services.AddSignalR();

  public static void MapLocationHub(this WebApplication app)
    => app.MapHub<LocationHub>("/current-location");
}
