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
        Description = "Profio API Documentation.",
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
