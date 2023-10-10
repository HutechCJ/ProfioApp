using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Exceptions;
using Serilog.Settings.Configuration;
using Serilog.Sinks.SystemConsole.Themes;

namespace Profio.Infrastructure.Logging;

public static class Extension
{
  public static void AddSerilog(this WebApplicationBuilder builder, string sectionName = "Serilog")
  {
    var serilogOptions = new Serilog();
    builder.Configuration.GetSection(sectionName).Bind(serilogOptions);

    builder.Host.UseSerilog((context, config) =>
    {
      config.ReadFrom.Configuration(
        context.Configuration,
        new ConfigurationReaderOptions { SectionName = sectionName }
      );

      config
        .Enrich.WithProperty("Application", builder.Environment.ApplicationName)
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails();

      if (serilogOptions.UseConsole)
        config.WriteTo.Async(writeTo =>
          writeTo.Console(outputTemplate: serilogOptions.LogTemplate, theme: AnsiConsoleTheme.Literate));

      if (serilogOptions.SeqUrl is { } && IsHostWorking(serilogOptions.SeqUrl))
        config.WriteTo.Async(writeTo => writeTo.Seq(serilogOptions.SeqUrl));
    });
  }

  private static bool IsHostWorking(string? host)
  {
    try
    {
      using var client = new HttpClient();
      var response = client.GetAsync(host).Result;
      return response.IsSuccessStatusCode;
    }
    catch
    {
      return false;
    }
  }
}
