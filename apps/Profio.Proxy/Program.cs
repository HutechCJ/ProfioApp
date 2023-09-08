using Profio.Infrastructure.OpenTelemetry;
using Spectre.Console;
using Profio.Infrastructure.Logging;

var builder = WebApplication.CreateBuilder(args);

AnsiConsole.Write(new FigletText("Profio Proxy").Centered().Color(Color.BlueViolet));

builder.AddSerilog();

builder.Services.AddHealthChecks();

builder.Services.AddReverseProxy()
  .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.AddOpenTelemetry();

var app = builder.Build();

app.MapReverseProxy();
app.MapHealthChecks("/health");
app.Run();
