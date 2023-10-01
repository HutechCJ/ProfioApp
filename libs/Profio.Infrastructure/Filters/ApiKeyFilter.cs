using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Profio.Infrastructure.Filters;

public class ApiKeyFilter : IOperationFilter
{
  public void Apply(OpenApiOperation operation, OperationFilterContext context)
  {
    if (!context.ApiDescription.CustomAttributes().Any(x => x is ApiKeyAttribute))
      return;

    operation.Summary = $"[ApiKey] {operation.Summary}";
  }
}
