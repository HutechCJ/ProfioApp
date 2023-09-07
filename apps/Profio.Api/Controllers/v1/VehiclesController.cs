using EntityFrameworkCore.Repository.Collections;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.Vehicles;
using Profio.Application.Vehicles.Commands;
using Profio.Application.Vehicles.Queries;
using Profio.Domain.Entities;
using Profio.Domain.Models;
using Profio.Domain.Specifications;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.Controllers.v1;

[ApiVersion("1.0")]
[SwaggerTag("Manage vehicles")]
public class VehiclesController : BaseEntityController<Vehicle, VehicleDto>
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
    => HandleCreateCommand(command, id => new GetVehicleByIdQuery(id));

  [HttpPut("{id:length(26)}")]
  [MapToApiVersion("1.0")]
  public Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdateVehicleCommand command)
    => HandleUpdateCommand(id, command);

  [HttpDelete("{id:length(26)}")]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<VehicleDto>>> Delete(string id)
    => HandleDeleteCommand(new DeleteVehicleCommand(id));
}
