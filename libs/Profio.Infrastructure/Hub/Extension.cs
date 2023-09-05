using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Profio.Infrastructure.Hub;

public static class Extension
{
  public static void AddSocketHub(this WebApplicationBuilder builder)
  {
    builder.Services.AddSignalR();
  }
}
