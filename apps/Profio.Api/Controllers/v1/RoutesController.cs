using EntityFrameworkCore.Repository.Collections;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.Routes;
using Profio.Application.Routes.Queries;
using Profio.Domain.Models;
using Profio.Domain.Specifications;
using Swashbuckle.AspNetCore.Annotations;
using RouteEntity = Profio.Domain.Entities.Route;

namespace Profio.Api.Controllers.v1;

[ApiVersion("1.0")]
[SwaggerTag("Manage routes between hubs")]
public class RoutesController : BaseEntityController<RouteEntity, RouteDto>
{
  [HttpGet]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<IPagedList<RouteDto>>>> Get([FromQuery] Criteria<RouteEntity> criteria)
    => HandlePaginationQuery(new GetRouteWithPagingQuery(criteria));

  [HttpGet("{id:length(26)}")]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<RouteDto>>> GetById(string id)
    => HandleGetByIdQuery(new GetRouteByIdQuery(id));
}
