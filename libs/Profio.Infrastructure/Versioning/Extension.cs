using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Profio.Infrastructure.Versioning;

public static class Extension
{
  public static void AddApiVersioning(this WebApplicationBuilder builder)
  {
    builder.Services.AddApiVersioning(options =>
    {
      options.ReportApiVersions = true;
      options.AssumeDefaultVersionWhenUnspecified = true;
      options.DefaultApiVersion = new(1, 0);
      //options.ApiVersionReader = new HeaderApiVersionReader("api-version");
    });

    builder.Services.AddVersionedApiExplorer(options =>
    {
      options.GroupNameFormat = "'v'VVV";
      options.SubstituteApiVersionInUrl = true;
    });
  }
}
