using Profio.Web.Application.Startup;
using Serilog;
using Spark.Library.Config;
using Spark.Library.Environment;
using Vite.AspNetCore.Extensions;

try
{
  EnvManager.LoadConfig();

  var builder = WebApplication.CreateBuilder(args);
  builder.Configuration.SetupSparkConfig();
  builder.Services.AddAppServices(builder.Configuration);

  var app = builder.Build();

  if (!app.Environment.IsDevelopment())
  {
    app.UseExceptionHandler("/Error");
    app.UseHsts();
  }

  if (app.Environment.IsDevelopment())
    app.UseViteDevMiddleware();

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
