using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Profio.Infrastructure.Swagger;

namespace Profio.Infrastructure;

public static class ConfigureServices
{
  public static void AddInfrastructureServices(this IServiceCollection services, WebApplicationBuilder builder)
  {
    services.AddControllers(options =>
      {
        options.RespectBrowserAcceptHeader = true;
        options.ReturnHttpNotAcceptable = true;
      })
      .AddNewtonsoftJson()
      .AddApplicationPart(AssemblyReference.Assembly);

    services.AddResponseCompression(options =>
      {
        options.EnableForHttps = true;
        options.Providers.Add<GzipCompressionProvider>();
        options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
        {
          "application/json",
          "application/xml",
          "text/plain",
          "image/png",
          "image/jpeg"
        });
      })
      .Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal)
      .AddResponseCaching(options => options.MaximumBodySize = 1024)
      .AddRouting(options => options.LowercaseUrls = true);

    services.AddCors(options => options
      .AddDefaultPolicy(policy => policy
        .WithOrigins("https://localhost:8080")
        .AllowAnyMethod()
        .AllowAnyHeader()));

    services.AddOpenApi();

    services.AddMediatR(config =>
      config.RegisterServicesFromAssembly(AssemblyReference.ExecuteAssembly)
    );

    services
      .AddProblemDetails()
      .AddEndpointsApiExplorer();
  }

  public static void UseWebInfrastructure(this WebApplication app)
  {

    app.UseCors()
      .UseExceptionHandler()
      .UseHttpsRedirection()
      .UseRateLimiter()
      .UseResponseCaching()
      .UseResponseCompression()
      .UseStatusCodePages()
      .UseStaticFiles();

    app.Map("/", () => Results.Redirect("/swagger"));
    app.Map("/error", () => Results.Problem("An unexpected error occurred.", statusCode: 500))
      .ExcludeFromDescription();
  }
}
