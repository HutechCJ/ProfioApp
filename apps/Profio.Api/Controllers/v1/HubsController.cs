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

namespace Profio.Api.Controllers.v1;

[Route("api/v1/[controller]")]
[ApiController]
[SwaggerTag("A hub endpoints")]
public class HubsController : BaseEntityController<Hub, HubDto, GetHubByIdQuery>
{
  [HttpGet]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<IPagedList<HubDto>>>> Get([FromQuery] Criteria<Hub> criteria)
    => HandlePaginationQuery(new GetHubWithPagingQuery(criteria));

  [HttpGet("{id:length(26)}")]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<HubDto>>> GetById(string id)
    => HandleGetByIdQuery(new GetHubByIdQuery(id));

  [HttpPost]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<HubDto>>> Post(CreateHubCommand command)
    => HandleCreateCommand(command);

  [HttpPut("{id:length(26)}")]
  [MapToApiVersion("1.0")]
  public Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdateHubCommand command)
    => HandleUpdateCommand(id, command);

  [HttpDelete("{id:length(26)}")]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<HubDto>>> Delete(string id)
    => HandleDeleteCommand(new DeleteHubCommand(id));

  [HttpGet("nearest-hub")]
  [MapToApiVersion("1.0")]
  public async Task<ActionResult<ResultModel<HubDto>>> GetNearestHub([FromQuery] Location location)
    => Ok(await Mediator.Send(location));
}
