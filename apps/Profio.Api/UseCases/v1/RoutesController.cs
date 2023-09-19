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
public sealed class RoutesController : BaseEntityController<RouteEntity, RouteDto, GetRouteByIdQuery>
{
  [HttpGet]
  [SwaggerOperation(summary: "Get Route List with Paging")]
  public Task<ActionResult<ResultModel<IPagedList<RouteDto>>>> Get([FromQuery] Criteria criteria)
    => HandlePaginationQuery(new GetRouteWithPagingQuery(criteria));

  [HttpGet("{id:length(26)}")]
  [SwaggerOperation(summary: "Get Route by Id")]
  public Task<ActionResult<ResultModel<RouteDto>>> GetById(string id)
    => HandleGetByIdQuery(new(id));

  [HttpPost]
  [SwaggerOperation(summary: "Create Route")]
  public Task<ActionResult<ResultModel<RouteDto>>> Post(CreateRouteCommand command)
    => HandleCreateCommand(command);

  [HttpPut("{id:length(26)}")]
  [SwaggerOperation(summary: "Update Route")]
  public Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdateRouteCommand command)
    => HandleUpdateCommand(id, command);

  [HttpDelete("{id:length(26)}")]
  [SwaggerOperation(summary: "Delete Route")]
  public Task<ActionResult<ResultModel<RouteDto>>> Delete(string id)
    => HandleDeleteCommand(new DeleteRouteCommand(id));
}
