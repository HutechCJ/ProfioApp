using Profio.Infrastructure.OpenTelemetry;
using Spectre.Console;

var builder = WebApplication.CreateBuilder(args);

AnsiConsole.Write(new FigletText("Profio Proxy").Centered().Color(Color.BlueViolet));

builder.WebHost.UseKestrel(options =>
{
  options.ConfigureHttpsDefaults(h =>
  {
    h.ClientCertificateMode = Microsoft.AspNetCore.Server.Kestrel.Https.ClientCertificateMode.RequireCertificate;
    h.ClientCertificateValidation = (certificate, _, _) => certificate.Subject.Contains("CN=Profio");
  });
  options.ListenAnyIP(80);
  options.ListenAnyIP(443);
});

builder.Services.AddReverseProxy()
  .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.AddOpenTelemetry();

var app = builder.Build();

app.MapReverseProxy();
app.Run();
