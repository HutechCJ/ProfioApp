using Profio.Api.Extensions;
using Serilog;
using Spectre.Console;

AnsiConsole.Write(new FigletText("Profio APIs").Centered().Color(Color.BlueViolet));

try
{
  var builder = WebApplication.CreateBuilder(args);

  builder.Services.AddControllers();

  var app = await builder
    .ConfigureServices()
    .ConfigurePipelineAsync();

  app.MapPrometheusScrapingEndpoint();

  app.Run();
}
catch (Exception ex)
{
  Log.Fatal(ex, "Unhandled Exception");
}
finally
{
  Log.Information("Shut down complete");
  Log.CloseAndFlush();
}
