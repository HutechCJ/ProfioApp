using Carter;
using Profio.Infrastructure.System;

namespace Profio.Api.UseCases.v2;

public class SystemEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("system", GetPlatform).WithName("Get System Platform");
  }

  private static IResult GetPlatform(IConfiguration config, IWebHostEnvironment env)
    => Results.Ok(config.GetPlatform(env));
}
