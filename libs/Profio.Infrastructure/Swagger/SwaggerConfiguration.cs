using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

namespace Profio.Infrastructure.Swagger;

public static class SwaggerConfiguration
{
  public static IApplicationBuilder UseOpenApi(this IApplicationBuilder app)
  {
    app.UseSwagger(c =>
    {
      c.RouteTemplate = "swagger/{documentName}/swagger.json";
      c.PreSerializeFilters.Add((swagger, httpReq) =>
      {
        ArgumentNullException.ThrowIfNull(httpReq, nameof(httpReq));

        swagger.Servers = new List<OpenApiServer>
        {
          new()
          {
            Url = $"{httpReq.Scheme}://{httpReq.Host.Value}"
          }
        };
      });
    });

    app.UseSwaggerUI(c =>
    {
      c.DocumentTitle = "Profio API";
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "Profio API");
      c.DisplayRequestDuration();
      c.EnableValidator();
    });

    app.UseReDoc(options =>
    {
      options.DocumentTitle = "Profio API";
      options.SpecUrl("/swagger/v1/swagger.json");
      options.EnableUntrustedSpec();
    });

    return app;
  }
}
