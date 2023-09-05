using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Profio.Infrastructure.Swagger;

public class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
{
  public void Configure(SwaggerGenOptions options)
  {
    options.SwaggerDoc("v1",
      new()
      {
        Title = "Profio API",
        Version = "v1",
        Description = "Profio - the symbol of professionalism in transportation management. Whether you need a solution for managing a fleet of vehicles or ships, Profio provides a powerful tool, optimizing and simplifying the process, ensuring every movement is quick, safe, and efficient. If you want to know more about Redocly of OpenAPI, please visit at [Here](https://localhost:9023/redoc).",
        License = new()
        {
          Name = "MIT",
          Url = new("https://opensource.org/licenses/MIT")
        }
      });

    options.AddSecurityDefinition("Bearer", new()
    {
      Name = "Authorization",
      Description = "Enter the Bearer Authorization string as following: `Generated-JWT-Token`",
      In = ParameterLocation.Header,
      Type = SecuritySchemeType.Http,
      Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new()
    {
      {
        new()
        {
          Name = JwtBearerDefaults.AuthenticationScheme,
          In = ParameterLocation.Header,
          Reference = new()
          {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
          }
        },
        new List<string>()
      }
    });

    options.ResolveConflictingActions(apiDescription => apiDescription.First());
    options.EnableAnnotations();
  }
}
