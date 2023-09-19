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
  [SwaggerOperation(summary: "Get Incident List with Paging")]
  public Task<ActionResult<ResultModel<IPagedList<IncidentDto>>>> Get([FromQuery] Criteria criteria, [FromQuery] IncidentEnumFilter incidentEnumFilter)
    => HandlePaginationQuery(new GetIncidentWithPagingQuery(criteria, incidentEnumFilter));

  [HttpGet("{id:length(26)}")]
  [SwaggerOperation(summary: "Get Incident")]
  public Task<ActionResult<ResultModel<IncidentDto>>> GetById(string id)
    => HandleGetByIdQuery(new(id));

  [HttpPost]
  [SwaggerOperation(summary: "Create Incident")]
  public Task<ActionResult<ResultModel<IncidentDto>>> Post(CreateIncidentCommand command)
    => HandleCreateCommand(command);

  [HttpPut("{id:length(26)}")]
  [SwaggerOperation(summary: "Update Incident")]
  public Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdateIncidentCommand command)
    => HandleUpdateCommand(id, command);

  [HttpDelete("{id:length(26)}")]
  [SwaggerOperation(summary: "Delete Incident")]
  public Task<ActionResult<ResultModel<IncidentDto>>> Delete(string id)
    => HandleDeleteCommand(new DeleteIncidentCommand(id));
  [HttpGet("count")]
  [SwaggerOperation(summary: "Get Incident count")]
  public async Task<ActionResult<ResultModel<int>>> GetCount()
    => Ok(ResultModel<int>.Create(await Mediator.Send(new GetIncidentCountQuery())));
}
