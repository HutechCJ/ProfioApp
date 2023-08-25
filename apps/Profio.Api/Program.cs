using Profio.Api.Extensions;
using Spectre.Console;

AnsiConsole.Write(new FigletText("Profio APIs").Centered().Color(Color.BlueViolet));

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = await builder
  .ConfigureServices()
  .ConfigurePipelineAsync();

app.Run();
