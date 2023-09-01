using Microsoft.AspNetCore.Mvc;
using Profio.Application.Routes.Queries;
using Profio.Domain.Specifications;
using RouteEntity = Profio.Domain.Entities.Route;

namespace Profio.Api.Controllers.v1;
[Route("api/[controller]")]
[ApiController]
public class RoutesController : BaseController
{
  [HttpGet()]
  public async Task<IActionResult> Get([FromQuery] Criteria<RouteEntity> criteria)
    => Ok(await Mediator.Send(new GetRouteWithPagingQuery(criteria)));
}
