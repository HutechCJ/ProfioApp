using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Profio.Infrastructure.Swagger;

public sealed class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
{
  private readonly IApiVersionDescriptionProvider _provider;

  public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider provider)
    => _provider = provider;

  public void Configure(SwaggerGenOptions options)
  {
    foreach (var description in _provider.ApiVersionDescriptions)
    {
      options.SwaggerDoc(description.GroupName,
        new()
        {
          Title = "Profio API",
          Version = description.ApiVersion.ToString(),
          Description =
            "Profio - the symbol of professionalism in transportation management. Whether you need a solution for managing a fleet of vehicles or ships, Profio provides a powerful tool, optimizing and simplifying the process, ensuring every movement is quick, safe, and efficient. The system is designed to be flexible and scalable, allowing you to manage your fleet of any size, from a few vehicles to thousands of ships. Profio is a product of CJ Logistics, a global leader in transportation and logistics services.",

          Contact = new()
          {
            Name = "HutechCJ",
            Url = new("https://github.com/HutechCJ")
          },

          License = new()
          {
            Name = "MIT",
            Url = new("https://opensource.org/licenses/MIT")
          },

          TermsOfService = new("https://www.cjlogistics.com/en/agreement/privacy-policy"),

          Extensions =
          {
            {
              "x-logo", new OpenApiObject
              {
                { "url", new OpenApiString("https://i.imgur.com/UAo6IJa.png") },
                { "altText", new OpenApiString("Profio API") },
                { "backgroundColor", new OpenApiString("#FFFFFF") },
                { "href", new OpenApiString("") }
              }
            }
          }
        });
    }

    options.AddSecurityDefinition("Bearer", new()
    {
      Name = "Authorization",
      Description = "Enter the Bearer Authorization string as following: `Generated-JWT-Token`",
      In = ParameterLocation.Header,
      Type = SecuritySchemeType.Http,
      Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityDefinition("ApiKey", new()
    {
      Description = "Enter the `API Key` that you get from the System Administrator",
      Type = SecuritySchemeType.ApiKey,
      Name = "X-API-Key",
      In = ParameterLocation.Header,
      Scheme = "ApiKeyScheme"
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
      },
      {
        new()
        {
          Reference = new()
          {
            Type = ReferenceType.SecurityScheme,
            Id = "ApiKey"
          },
          In = ParameterLocation.Header
        },
        new List<string>()
      }
    });

    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
    {
      options.AddSecurityDefinition("OAuth2", new()
      {
        Type = SecuritySchemeType.OAuth2,
        Description = "OAuth2 Authorization Code Flow. SSO with `Keycloak`.",
        Flows = new()
        {
          Implicit = new()
          {
            AuthorizationUrl = new("http://localhost:8090/auth/realms/master/protocol/openid-connect/token"),
            Scopes = new Dictionary<string, string>
            {
              { "readAccess", "Access read operations" },
              { "writeAccess", "Access write operations" }
            }
          }
        }
      });

      options.AddSecurityRequirement(new()
      {
        {
          new()
          {
            Reference = new()
            {
              Type = ReferenceType.SecurityScheme,
              Id = "OAuth2"
            }
          },
          new List<string>()
        }
      });
    }

    options.ResolveConflictingActions(apiDescription => apiDescription.First());
    options.EnableAnnotations();
  }
}
