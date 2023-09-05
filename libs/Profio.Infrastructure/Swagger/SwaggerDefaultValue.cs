using System.Text.Json;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Profio.Infrastructure.Swagger;

public class SwaggerDefaultValues : IOperationFilter
{
  public const string VersionPattern = "v{version:apiVersion}";

  public void Apply(OpenApiOperation operation, OperationFilterContext context)
  {
    var apiDescription = context.ApiDescription;

    context.ApiDescription.RelativePath = context.ApiDescription.RelativePath?
      .Replace(VersionPattern, context.ApiDescription.GroupName);

    foreach (var responseType in context.ApiDescription.SupportedResponseTypes)
    {
      var responseKey = responseType.IsDefaultResponse
        ? "default"
        : responseType.StatusCode.ToString();

      var response = operation.Responses[responseKey];

      foreach (var contentType in response.Content.Keys)
        if (responseType.ApiResponseFormats.All(x => x.MediaType != contentType))
          response.Content.Remove(contentType);
    }


    if (operation.Parameters is null) return;

    foreach (var parameter in operation.Parameters)
    {
      var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

      parameter.Description ??= description.ModelMetadata?.Description;

      if (parameter.Schema.Default is null
          && description.DefaultValue is { })
        parameter.Schema.Default = OpenApiAnyFactory
          .CreateFromJson(JsonSerializer
            .Serialize(description.DefaultValue, description.ModelMetadata?.ModelType ?? typeof(object)));

      parameter.Required |= description.IsRequired;
    }
  }
}
