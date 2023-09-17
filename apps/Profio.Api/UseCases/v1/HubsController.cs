using EntityFrameworkCore.Repository.Collections;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.Hubs;
using Profio.Application.Hubs.Commands;
using Profio.Application.Hubs.Queries;
using Profio.Domain.Entities;
using Profio.Domain.Models;
using Profio.Domain.Specifications;
using Profio.Domain.ValueObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.UseCases.v1;

[ApiVersion("1.0")]
[SwaggerTag("Manage Hubs")]
public class HubsController : BaseEntityController<Hub, HubDto, GetHubByIdQuery>
{
  [HttpGet]
  [SwaggerOperation(summary: "Get Hub List with Paging")]
  public Task<ActionResult<ResultModel<IPagedList<HubDto>>>> Get([FromQuery] Criteria criteria, [FromQuery] HubEnumFilter hubEnumFilter)
    => HandlePaginationQuery(new GetHubWithPagingQuery(criteria, hubEnumFilter));

  [HttpGet("{id:length(26)}")]
  [SwaggerOperation(summary: "Get Hub by Id")]
  public Task<ActionResult<ResultModel<HubDto>>> GetById(string id)
    => HandleGetByIdQuery(new(id));

  [HttpPost]
  [SwaggerOperation(summary: "Create Hub")]
  public Task<ActionResult<ResultModel<HubDto>>> Post(CreateHubCommand command)
    => HandleCreateCommand(command);

  [HttpPut("{id:length(26)}")]
  [SwaggerOperation(summary: "Update Hub")]
  public Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdateHubCommand command)
    => HandleUpdateCommand(id, command);

  [HttpDelete("{id:length(26)}")]
  [SwaggerOperation(summary: "Delete Hub")]
  public Task<ActionResult<ResultModel<HubDto>>> Delete(string id)
    => HandleDeleteCommand(new DeleteHubCommand(id));

  [HttpGet("nearest-hub")]
  [SwaggerOperation(summary: "Get nearest Hub")]
  public async Task<ActionResult<ResultModel<HubDto>>> GetNearestHub([FromQuery] Location location)
    => Ok(ResultModel<HubDto>.Create(await Mediator.Send(new GetNearestHubByLocationQuery(location))));

}
