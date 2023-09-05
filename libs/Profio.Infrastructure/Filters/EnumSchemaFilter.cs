using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Profio.Infrastructure.Filters;

public class EnumSchemaFilter : ISchemaFilter
{
  public void Apply(OpenApiSchema schema, SchemaFilterContext context)
  {
    if (!context.Type.IsEnum)
      return;

    schema.Enum.Clear();
    Enum.GetNames(context.Type)
      .ToList()
      .ForEach(name => schema.Enum.Add(new OpenApiString($"{name}")));
  }
}
