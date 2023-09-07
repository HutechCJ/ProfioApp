using Profio.Infrastructure.OpenTelemetry;
using Spectre.Console;

var builder = WebApplication.CreateBuilder(args);

AnsiConsole.Write(new FigletText("Profio Proxy").Centered().Color(Color.BlueViolet));

builder.Services.AddHealthChecks();

builder.Services.AddReverseProxy()
  .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.AddOpenTelemetry();

var app = builder.Build();

app.MapReverseProxy();
app.MapHealthChecks("/health");
app.Run();
