using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
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
            Url = $"{httpReq.Scheme}://{httpReq.Host.Value}",
            Description = "Staging Environment"
          }
        };

        swagger.ExternalDocs = new()
        {
          Description = "Github",
          Url = new("https://github.com/HutechCJ/ProfioApp/")
        };
      });
    });

    app.UseSwaggerUI(c =>
    {
      c.DocumentTitle = "Profio API";
      c.InjectStylesheet("/css/swagger-ui.css");
      foreach (var description in app.ApplicationServices
                 .GetRequiredService<IApiVersionDescriptionProvider>()
                 .ApiVersionDescriptions)
        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
          description.GroupName.ToUpperInvariant());
      c.DisplayRequestDuration();
      c.EnableValidator();
    });

    return app;
  }

  public static IApplicationBuilder UseRedocly(this IApplicationBuilder app)
  {
    app.UseReDoc(options =>
    {
      options.DocumentTitle = "Profio API";
      foreach (var description in app.ApplicationServices
                 .GetRequiredService<IApiVersionDescriptionProvider>()
                 .ApiVersionDescriptions)
        options.SpecUrl($"/swagger/{description.GroupName}/swagger.json");

      options.EnableUntrustedSpec();
    });

    return app;
  }
}
