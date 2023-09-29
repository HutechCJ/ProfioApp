using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Profio.Infrastructure.Delegate;

namespace Profio.Infrastructure.Http;
public static class Extension
{
  public static WebApplicationBuilder AddHttpRestClient(this WebApplicationBuilder builder)
  {
    builder.Services.AddTransient<RetryDelegate>();
    builder.Services.AddTransient<LoggingDelegate>();

    builder.Services.AddHttpClient("Api",
      config =>
        config.BaseAddress = new(
          //builder.Configuration["ApiUrl"] ??
          "https://localhost:9023/api/v1/")
    )
      .AddHttpMessageHandler<RetryDelegate>()
      .AddHttpMessageHandler<LoggingDelegate>();

    return builder;
  }
}
