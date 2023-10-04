using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetCore.AutoRegisterDi;
using Profio.Infrastructure.Auth;
using Profio.Infrastructure.Bus;
using Profio.Infrastructure.Bus.MQTT;
using Profio.Infrastructure.Cache;
using Profio.Infrastructure.Email;
using Profio.Infrastructure.Filters;
using Profio.Infrastructure.HealthCheck;
using Profio.Infrastructure.Http;
using Profio.Infrastructure.Hub;
using Profio.Infrastructure.Jobs;
using Profio.Infrastructure.Key;
using Profio.Infrastructure.Logging;
using Profio.Infrastructure.Message;
using Profio.Infrastructure.Middleware;
using Profio.Infrastructure.OpenTelemetry;
using Profio.Infrastructure.Persistence;
using Profio.Infrastructure.Persistence.Idempotency;
using Profio.Infrastructure.Searching;
using Profio.Infrastructure.Storage;
using Profio.Infrastructure.Swagger;
using Profio.Infrastructure.Versioning;
using Twilio.Clients;

namespace Profio.Infrastructure;

public static class ConfigureServices
{
  public static void AddInfrastructureServices(this IServiceCollection services, WebApplicationBuilder builder)
  {
    builder.AddApiVersioning();
    builder.AddSerilog("Profio Api");
    builder.AddOpenTelemetry();
    builder.AddHealthCheck();
    builder.AddSocketHub();
    builder.AddLuceneSearch();
    builder.AddHttpRestClient();
    builder.AddBackgroundJob();

    services
      .AddProblemDetails()
      .AddEndpointsApiExplorer()
      .AddOpenApi();

    services.RegisterAssemblyPublicNonGenericClasses()
      .Where(c => c.Name.EndsWith("Service"))  //optional
      .IgnoreThisInterface<IMqttClientService>()
      .AsPublicImplementedInterfaces();

    services.AddSingleton<IDeveloperPageExceptionFilter, DeveloperPageExceptionFilter>();
    services.AddScoped<ITokenService, TokenService>();
    services.AddScoped<IIdempotencyService, IdempotencyService>();
    services.AddHttpClient<ITwilioRestClient, TwilioClient>();

    services.AddScoped<ClientIpCheckActionFilter>(container => new(
      container.GetRequiredService<ILogger<ClientIpCheckActionFilter>>(), builder.Configuration["AdminSafeList"]));

    services
      .AddPostgres(builder.Configuration)
      .AddRedisCache(builder.Configuration)
      .AddEmailSender(builder.Configuration)
      .AddEventBus(builder.Configuration);

    services.AddMqttBus(builder.Configuration);
    services.AddStorage(builder.Configuration);

    services.AddApplicationIdentity(builder, builder.Configuration);

    services.AddApiKey();
  }

  public static async Task UseWebInfrastructureAsync(this WebApplication app)
  {
    if (app.Environment.IsDevelopment())
    {
      using var scope = app.Services.CreateScope();
      var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
      await initializer.InitialiseAsync();
      await initializer.SeedAsync();
    }

    app
      .UseAuthentication()
      .UseAuthorization();

    app
      .UseMiddleware<TimeOutMiddleware>()
      .UseMiddleware<XssProtectionMiddleware>();

    app.MapLocationHub();

    app
      .UseCors()
      .UseExceptionHandler()
      .UseHttpsRedirection()
      .UseCookiePolicy()
      .UseRateLimiter()
      .UseResponseCaching()
      .UseResponseCompression()
      .UseStatusCodePages()
      .UseStaticFiles();

    app.MapHealthCheck();
    app.Map("/", () => Results.Redirect("/swagger"));
    app.Map("/error",
        () => Results.Problem("An unexpected error occurred.", statusCode: StatusCodes.Status500InternalServerError))
      .ExcludeFromDescription();
  }
}
