using EntityFrameworkCore.Repository.Collections;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.Incidents;
using Profio.Application.Incidents.Commands;
using Profio.Application.Incidents.Queries;
using Profio.Domain.Entities;
using Profio.Domain.Models;
using Profio.Domain.Specifications;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.UseCases.v1;

[ApiVersion("1.0")]
[SwaggerTag("Manage incidents")]
public sealed class IncidentsController : BaseEntityController<Incident, IncidentDto, GetIncidentByIdQuery>
{
  [HttpGet]
  [SwaggerOperation("Get Incident List with Paging")]
  public Task<ActionResult<ResultModel<IPagedList<IncidentDto>>>> Get([FromQuery] Specification specification,
    [FromQuery] IncidentEnumFilter incidentEnumFilter)
    => HandlePaginationQuery(new GetIncidentWithPagingQuery(specification, incidentEnumFilter));

  [HttpGet("{id:length(26)}")]
  [SwaggerOperation("Get Incident")]
  public Task<ActionResult<ResultModel<IncidentDto>>> GetById(string id)
    => HandleGetByIdQuery(new(id));

  [HttpPost]
  [SwaggerOperation("Create Incident")]
  public Task<ActionResult<ResultModel<IncidentDto>>> Post(CreateIncidentCommand command)
    => HandleCreateCommand(command);

  [HttpPut("{id:length(26)}")]
  [SwaggerOperation("Update Incident")]
  public Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdateIncidentCommand command)
    => HandleUpdateCommand(id, command);

  [HttpDelete("{id:length(26)}")]
  [SwaggerOperation("Delete Incident")]
  public Task<ActionResult<ResultModel<IncidentDto>>> Delete(string id)
    => HandleDeleteCommand(new DeleteIncidentCommand(id));

  [HttpGet("count")]
  [SwaggerOperation("Get Incident count")]
  public async Task<ActionResult<ResultModel<int>>> GetCount()
    => Ok(ResultModel<int>.Create(await Mediator.Send(new GetIncidentCountQuery())));

  [HttpPatch("{id:length(26)}/update-status")]
  [SwaggerOperation("Update Incident status")]
  public async Task<IActionResult> UpdateStatus([FromRoute] string id, [FromBody] UpdateIncidentStatusCommand command)
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
