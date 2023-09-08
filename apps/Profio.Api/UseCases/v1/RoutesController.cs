using EntityFrameworkCore.Repository.Collections;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.Routes;
using Profio.Application.Routes.Commands;
using Profio.Application.Routes.Queries;
using Profio.Domain.Models;
using Profio.Domain.Specifications;
using Swashbuckle.AspNetCore.Annotations;
using RouteEntity = Profio.Domain.Entities.Route;

namespace Profio.Api.UseCases.v1;

[ApiVersion("1.0")]
[SwaggerTag("Manage routes between hubs")]
public class RoutesController : BaseEntityController<RouteEntity, RouteDto, GetRouteByIdQuery>
{
  [HttpGet]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<IPagedList<RouteDto>>>> Get([FromQuery] Criteria<RouteEntity> criteria)
    => HandlePaginationQuery(new GetRouteWithPagingQuery(criteria));

  [HttpGet("{id:length(26)}")]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<RouteDto>>> GetById(string id)
    => HandleGetByIdQuery(new GetRouteByIdQuery(id));

  [HttpPost]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<RouteDto>>> Post(CreateRouteCommand command)
    => HandleCreateCommand(command);

  [HttpPut("{id:length(26)}")]
  [MapToApiVersion("1.0")]
  public Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdateRouteCommand command)
    => HandleUpdateCommand(id, command);

  [HttpDelete("{id:length(26)}")]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<RouteDto>>> Delete(string id)
    => HandleDeleteCommand(new DeleteRouteCommand(id));
}
