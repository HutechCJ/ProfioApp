using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Profio.Infrastructure.Swagger;

public static class Extension
{
  public static IServiceCollection AddOpenApi(this IServiceCollection services)
    => services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>()
      .AddFluentValidationRulesToSwagger()
      .AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>())
      .AddSwaggerGenNewtonsoftSupport()
      .Configure<SwaggerGeneratorOptions>(o => o.InferSecuritySchemes = true);
}
