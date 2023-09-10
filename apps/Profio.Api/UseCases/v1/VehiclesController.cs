using EntityFrameworkCore.Repository.Collections;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.Hubs;
using Profio.Application.Hubs.Queries;
using Profio.Application.Vehicles;
using Profio.Application.Vehicles.Commands;
using Profio.Application.Vehicles.Queries;
using Profio.Domain.Entities;
using Profio.Domain.Models;
using Profio.Domain.Specifications;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.UseCases.v1;

[ApiVersion("1.0")]
[SwaggerTag("Manage vehicles")]
public class VehiclesController : BaseEntityController<Vehicle, VehicleDto, GetVehicleByIdQuery>
{
  [HttpGet]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<IPagedList<VehicleDto>>>> Get([FromQuery] Criteria<Vehicle> criteria)
    => HandlePaginationQuery(new GetVehicleWithPagingQuery(criteria));

  [HttpGet("{id:length(26)}")]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<VehicleDto>>> GetById(string id)
    => HandleGetByIdQuery(new GetVehicleByIdQuery(id));

  [HttpPost]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<VehicleDto>>> Post(CreateVehicleCommand command)
    => HandleCreateCommand(command);

  [HttpPut("{id:length(26)}")]
  [MapToApiVersion("1.0")]
  public Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdateVehicleCommand command)
    => HandleUpdateCommand(id, command);

  [HttpDelete("{id:length(26)}")]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<VehicleDto>>> Delete(string id)
    => HandleDeleteCommand(new DeleteVehicleCommand(id));
  [HttpGet("{id:length(26)}/hubs/next")]
  public async Task<ActionResult<ResultModel<HubDto>>> GetNextHub(string id)
    => ResultModel<HubDto>.Create(await Mediator.Send(new GetNextHubByVehicleIdQuery(id)));
  [HttpGet("count-by-type")]
  [MapToApiVersion("1.0")]
  public async Task<ActionResult<ResultModel<IEnumerable<int>>>> GetCountByType()
    => Ok(ResultModel<IEnumerable<int>>.Create(await Mediator.Send(new GetVehicleCountByTypeQuery())));
}
