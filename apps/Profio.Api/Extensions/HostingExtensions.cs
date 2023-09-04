using Microsoft.AspNetCore.Server.Kestrel.Core;
using Profio.Application;
using Profio.Infrastructure;
using Profio.Infrastructure.Swagger;

namespace Profio.Api.Extensions;

public static class HostingExtensions
{
  public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
  {
    builder.Services.AddInfrastructureServices(builder);
    builder.Services.AddApplicationServices();
    builder.Services.AddRateLimiting();

    builder.WebHost.ConfigureKestrel(options =>
    {
      options.AddServerHeader = false;
      options.AllowResponseHeaderCompression = true;
      options.ConfigureEndpointDefaults(o => o.Protocols = HttpProtocols.Http1AndHttp2AndHttp3);
    });

    return builder.Build();
  }

  public static async Task<WebApplication> ConfigurePipelineAsync(this WebApplication app)
  {
    if (app.Environment.IsDevelopment())
      app.UseOpenApi()
        .UseDeveloperExceptionPage()
        .UseHsts();
    else
      app.UseExceptionHandler("/error");

    app.UseHttpsRedirection();
    //app.UseAuthorization();
    app.MapControllers()
      .RequirePerUserRateLimit();
    app.UseRateLimiter();
    await app.UseWebInfrastructureAsync();

    return app;
  }
}
