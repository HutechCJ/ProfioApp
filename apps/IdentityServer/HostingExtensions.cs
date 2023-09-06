using Duende.IdentityServer;
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
      .AddIdentityServer(options =>
      {
        options.Events.RaiseErrorEvents = true;
        options.Events.RaiseInformationEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseSuccessEvents = true;
        options.EmitStaticAudienceClaim = true;
      })
      .AddInMemoryIdentityResources(Config.IdentityResources)
      .AddInMemoryApiScopes(Config.ApiScopes)
      .AddInMemoryClients(Config.Clients)
      .AddAspNetIdentity<ApplicationUser>();

    builder.Services.AddAuthentication()
      .AddGoogle(options =>
      {
        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

        // register your IdentityServer with Google at https://console.developers.google.com
        // enable the Google+ API
        // set the redirect URI to https://localhost:5001/signin-google
        options.ClientId = "copy client ID from Google here";
        options.ClientSecret = "copy client secret from Google here";
      });

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
