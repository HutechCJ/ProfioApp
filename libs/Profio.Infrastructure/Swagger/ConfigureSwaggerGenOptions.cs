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
        Description = "Profio Application, a blend of profile and portfolio, is a versatile platform for crafting and overseeing personal portfolios. Users can tailor themes, document educational and career milestones, and incorporate external links. Tech aficionados benefit from a distinct tech stack showcase. Enhanced with drag-and-drop functionality, all user data is anchored in a sophisticated database structure. If you want to know more about Redocly of OpenAPI, please visit at [Here](https://localhost:9023/redoc).",
        License = new()
        {
          Name = "MIT",
          Url = new("https://opensource.org/licenses/MIT")
        }
      });

    options.AddSecurityDefinition("Bearer", new()
    {
      Name = "Authorization",
      Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
      In = ParameterLocation.Header,
      Type = SecuritySchemeType.ApiKey,
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
