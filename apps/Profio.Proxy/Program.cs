using Profio.Infrastructure.Logging;
using Profio.Infrastructure.OpenTelemetry;
using Serilog;
using Spectre.Console;

AnsiConsole.Write(new FigletText("Profio Proxy").Centered().Color(Color.BlueViolet));

try
{
  var builder = WebApplication.CreateBuilder(args);

  builder.AddSerilog("Profio.Proxy");

  builder.Services.AddHealthChecks();

  builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

  builder.AddOpenTelemetry();

  var app = builder.Build();

  app.MapReverseProxy();
  app.MapHealthChecks("/health");
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
