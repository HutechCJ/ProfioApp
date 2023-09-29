using Profio.Api.Extensions;
using Serilog;
using Spectre.Console;

AnsiConsole.Write(new FigletText("Profio APIs").Centered().Color(Color.BlueViolet));

try
{
  var builder = WebApplication.CreateBuilder(args);

  var app = await builder
    .ConfigureServices()
    .ConfigurePipelineAsync();

  app.MapPrometheusScrapingEndpoint();

  app.Run();
}
catch (Exception ex)
  when (ex.GetType().Name is not "StopTheHostException"
  && ex.GetType().Name is not "HostAbortedException")
{
  Log.Fatal(ex, "Unhandled exception");
}
catch (AggregateException ex)
{
  throw ex.Flatten();
}
finally
{
  Log.Information("Shut down complete");
  Log.CloseAndFlush();
}
