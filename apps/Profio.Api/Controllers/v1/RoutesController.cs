using EntityFrameworkCore.Repository.Collections;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.Routes;
using Profio.Application.Routes.Queries;
using Profio.Domain.Models;
using Profio.Domain.Specifications;
using RouteEntity = Profio.Domain.Entities.Route;

namespace Profio.Api.Controllers.v1;

[ApiVersion("1.0")]
public class RoutesController : BaseEntityController<RouteEntity, RouteDTO>
{
  [HttpGet]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<IPagedList<RouteDTO>>>> Get([FromQuery] Criteria<RouteEntity> criteria)
    => HandlePaginationQuery(new GetRouteWithPagingQuery(criteria));
  [HttpGet("{id}")]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<RouteDTO>>> GetById(string id)
    => HandleGetByIdQuery(new GetRouteByIdQuery(id));
}
