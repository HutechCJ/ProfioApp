using Microsoft.AspNetCore.Mvc;
using Profio.Application.Counters.Queries;
using Profio.Domain.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.UseCases.v1;

[ApiVersion("1.0")]
[SwaggerTag("Manage counters")]
public class CountersController : BaseController
{

  [HttpGet("entities")]
  [MapToApiVersion("1.0")]
  public async Task<ActionResult<ResultModel<Dictionary<string, int>>>> GetEntityCount([FromQuery] IList<string> entityTypes)
  {
    return Ok(ResultModel<Dictionary<string, int>>.Create(await Mediator.Send(new GetEntityCountQuery(entityTypes))));
  }
}
