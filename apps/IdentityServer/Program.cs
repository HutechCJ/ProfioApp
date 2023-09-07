using IdentityServer;
using Serilog;
using Spectre.Console;

AnsiConsole.Write(new FigletText("Profio IdentityServer").Centered().Color(Color.BlueViolet));

Log.Logger = new LoggerConfiguration()
  .WriteTo.Console()
  .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
  var builder = WebApplication.CreateBuilder(args);

  builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(ctx.Configuration));

  var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

  app.Run();
}
catch (Exception ex)
  when(
    ex.GetType().Name is not "StopTheHostException"
    && ex.GetType().Name is not "HostAbortedException")
{
  Log.Fatal(ex, "Unhandled exception");
}
finally
{
  Log.Information("Shut down complete");
  Log.CloseAndFlush();
}
