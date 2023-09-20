using EntityFrameworkCore.Repository.Collections;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.Phases;
using Profio.Application.Phases.Commands;
using Profio.Application.Phases.Queries;
using Profio.Domain.Entities;
using Profio.Domain.Models;
using Profio.Domain.Specifications;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.UseCases.v1;

[ApiVersion("1.0")]
[SwaggerTag("Manage routes between hubs")]
public sealed class PhasesController : BaseEntityController<Phase, PhaseDto, GetPhaseByIdQuery>
{
  [HttpGet]
  [SwaggerOperation(summary: "Get Phase List with Paging")]
  public Task<ActionResult<ResultModel<IPagedList<PhaseDto>>>> Get([FromQuery] Criteria criteria)
    => HandlePaginationQuery(new GetPhaseWithPagingQuery(criteria));

  [HttpGet("{id:length(26)}")]
  [SwaggerOperation(summary: "Get Phase by Id")]
  public Task<ActionResult<ResultModel<PhaseDto>>> GetById(string id)
    => HandleGetByIdQuery(new(id));

  [HttpPost]
  [SwaggerOperation(summary: "Create Phase")]
  public Task<ActionResult<ResultModel<PhaseDto>>> Post(CreatePhaseCommand command)
    => HandleCreateCommand(command);

  [HttpPut("{id:length(26)}")]
  [SwaggerOperation(summary: "Update Phase")]
  public Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdatePhaseCommand command)
    => HandleUpdateCommand(id, command);

  [HttpDelete("{id:length(26)}")]
  [SwaggerOperation(summary: "Delete Phase")]
  public Task<ActionResult<ResultModel<PhaseDto>>> Delete(string id)
    => HandleDeleteCommand(new DeletePhaseCommand(id));
}
