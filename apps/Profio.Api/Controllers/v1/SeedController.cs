using Microsoft.AspNetCore.Mvc;
using Profio.Application.Seed.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.Controllers.v1;

[ApiVersion("1.0")]
[SwaggerTag("Seed Data")]
public class SeedController : BaseController
{
  [HttpGet]
  [MapToApiVersion("1.0")]
  public async Task<IActionResult> GetNotFoundAsync()
    => Ok(await Mediator.Send(new SeedDataQuery()));
}
