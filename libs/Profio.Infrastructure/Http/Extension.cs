using Profio.Infrastructure.Delegate;

namespace Profio.Infrastructure.Http;
public static class Extension
{
  public static WebApplicationBuilder AddHttpRestClient(this WebApplicationBuilder builder)
  {
    builder.Services.AddTransient<RetryDelegate>();
    builder.Services.AddTransient<LoggingDelegate>();
    builder.Services.AddTransient<AuthenticationDelegate>();

    builder.Services.AddHttpClient("Api",
      config =>
        config.BaseAddress = new(builder.Configuration["ApiUrl"] ?? "https://localhost:9023/api/v1/")
    )
      .AddHttpMessageHandler<AuthenticationDelegate>()
      .AddHttpMessageHandler<RetryDelegate>()
      .AddHttpMessageHandler<LoggingDelegate>();

    return builder;
  }
}
