using Microsoft.AspNetCore.Identity;
using Profio.Infrastructure.Identity;
using Profio.Infrastructure.OpenTelemetry;
using Profio.Infrastructure.Persistence;
using Serilog;

namespace IdentityServer;

public static class HostingExtensions
{
  public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
  {

    builder.AddOpenTelemetry();

    builder.Services.AddRazorPages();

    builder.Services.AddPostgres(builder.Configuration);

    builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
      .AddEntityFrameworkStores<ApplicationDbContext>()
      .AddDefaultTokenProviders();

    builder.Services
      .AddIdentityServer()
      .AddInMemoryIdentityResources(Config.IdentityResources)
      .AddInMemoryApiScopes(Config.ApiScopes)
      .AddInMemoryClients(Config.Clients)
      .AddAspNetIdentity<ApplicationUser>();

    return builder.Build();
  }

  public static WebApplication ConfigurePipeline(this WebApplication app)
  {
    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
      app.UseDeveloperExceptionPage();
    }

    app.UseStaticFiles();
    app.UseRouting();
    app.UseIdentityServer();
    app.UseAuthorization();

    app.MapRazorPages()
      .RequireAuthorization();

    return app;
  }
}
