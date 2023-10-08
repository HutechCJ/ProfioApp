using CurrieTechnologies.Razor.SweetAlert2;
using Profio.Infrastructure.Http;
using Profio.Infrastructure.Logging;
using Profio.Infrastructure.OpenTelemetry;
using Profio.Website.Cache;
using Profio.Website.Middleware;
using Profio.Website.Repositories;
using Profio.Website.Services;
using Radzen;
using Serilog;
using Spectre.Console;

AnsiConsole.Write(new FigletText("Profio Website").Centered().Color(Color.BlueViolet));

try
{
  var builder = WebApplication.CreateBuilder(args);

  builder.Services.AddRazorPages();
  builder.Services.AddServerSideBlazor();
  builder.Services.AddSweetAlert2();
  builder.Services.AddMemoryCache();
  builder.Services.AddRadzenComponents();

  builder.AddOpenTelemetry();
  builder.AddSerilog("Profio.Website");
  builder.AddHttpRestClient();

  builder.Services.AddSingleton<IRepository, Repository>();
  builder.Services.AddSingleton<ICacheService, CacheService>();
  builder.Services.AddTransient<ICustomerService, CustomerService>();

  var app = builder.Build();

  if (!app.Environment.IsDevelopment())
  {
    app.UseExceptionHandler("/Error");
    app.UseHsts();
  }

  app.UseRobotsTxtMiddleware();
  app.UseHttpsRedirection();
  app.UseStaticFiles();
  app.UseRouting();
  app.MapBlazorHub();
  app.MapFallbackToPage("/_Host");

  app.Run();
}
catch (Exception ex)
  when (ex.GetType().Name is not "StopTheHostException"
        && ex.GetType().Name is not "HostAbortedException")
{
  Log.Fatal(ex, "Unhandled exception");
}
finally
{
  Log.Information("Shut down complete");
  Log.CloseAndFlush();
}
