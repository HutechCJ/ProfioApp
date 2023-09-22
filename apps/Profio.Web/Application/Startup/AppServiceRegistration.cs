using Coravel;
using Spark.Library.Logging;
using Vite.AspNetCore.Extensions;

namespace Profio.Web.Application.Startup;

public static class AppServiceRegistration
{
  public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration config)
  {
    services.AddViteServices();
    services.AddRazorPages();
    services.AddServerSideBlazor();
    services.AddLogger(config);
    services.AddEvents();
    return services;
  }
}
