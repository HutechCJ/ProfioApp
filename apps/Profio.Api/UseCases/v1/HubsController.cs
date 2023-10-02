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
public sealed class HubsController : BaseEntityController<Hub, HubDto, GetHubByIdQuery>
{
  [HttpGet]
  [SwaggerOperation("Get Hub List with Paging")]
  public Task<ActionResult<ResultModel<IPagedList<HubDto>>>> Get([FromQuery] Specification specification,
    [FromQuery] HubEnumFilter hubEnumFilter)
    => HandlePaginationQuery(new GetHubWithPagingQuery(specification, hubEnumFilter));

  [HttpGet("{id:length(26)}")]
  [SwaggerOperation("Get Hub by Id")]
  public Task<ActionResult<ResultModel<HubDto>>> GetById(string id)
    => HandleGetByIdQuery(new(id));

  [HttpPost]
  [SwaggerOperation("Create Hub")]
  public Task<ActionResult<ResultModel<HubDto>>> Post(CreateHubCommand command)
    => HandleCreateCommand(command);

  [HttpPut("{id:length(26)}")]
  [SwaggerOperation("Update Hub")]
  public Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdateHubCommand command)
    => HandleUpdateCommand(id, command);

  [HttpDelete("{id:length(26)}")]
  [SwaggerOperation("Delete Hub")]
  public Task<ActionResult<ResultModel<HubDto>>> Delete(string id)
    => HandleDeleteCommand(new DeleteHubCommand(id));

  [HttpGet("nearest-hub")]
  [SwaggerOperation("Get nearest Hub")]
  public async Task<ActionResult<ResultModel<HubDto>>> GetNearestHub([FromQuery] Location location)
    => Ok(ResultModel<HubDto>.Create(await Mediator.Send(new GetNearestHubByLocationQuery(location))));

  [HttpPatch("{id:length(26)}/update-status")]
  [SwaggerOperation("Update Hub status")]
  public async Task<IActionResult> UpdateStatus([FromRoute] string id, [FromBody] UpdateHubStatusCommand command)
  {
    if (!id.Equals(command.Id))
    {
      ModelState.AddModelError("Id", "Ids are not the same");
      return ValidationProblem();
    }

    await Mediator.Send(command).ConfigureAwait(false);
    return NoContent();
  }
}
