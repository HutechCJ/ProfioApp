using Microsoft.AspNetCore.Mvc;
using Profio.Application.Routes.Queries;
using Profio.Domain.Specifications;
using RouteEntity = Profio.Domain.Entities.Route;

namespace Profio.Api.Controllers.v1;

[ApiVersion("1.0")]
public class RoutesController : BaseController
{
  [HttpGet]
  [MapToApiVersion("1.0")]
  public async Task<IActionResult> Get([FromQuery] Criteria<RouteEntity> criteria)
    => Ok(await Mediator.Send(new GetRouteWithPagingQuery(criteria)));
  [HttpGet("{id}")]
  [MapToApiVersion("1.0")]
  public async Task<IActionResult> GetById(string id)
    => Ok(await Mediator.Send(new GetRouteByIdQuery(id)));
}
