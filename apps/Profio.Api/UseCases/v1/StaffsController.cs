using EntityFrameworkCore.Repository.Collections;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.Staffs;
using Profio.Application.Staffs.Commands;
using Profio.Application.Staffs.Queries;
using Profio.Domain.Entities;
using Profio.Domain.Models;
using Profio.Domain.Specifications;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.UseCases.v1;

[ApiVersion("1.0")]
[SwaggerTag("Manage staffs")]
public class StaffsController : BaseEntityController<Staff, StaffDto, GetStaffByIdQuery>
{
  [HttpGet]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<IPagedList<StaffDto>>>> Get([FromQuery] Criteria<Staff> criteria)
    => HandlePaginationQuery(new GetStaffWithPagingQuery(criteria));

  [HttpGet("{id:length(26)}")]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<StaffDto>>> GetById(string id)
    => HandleGetByIdQuery(new GetStaffByIdQuery(id));

  [HttpPost]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<StaffDto>>> Post(CreateStaffCommand command)
    => HandleCreateCommand(command);

  [HttpPut("{id:length(26)}")]
  [MapToApiVersion("1.0")]
  public Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdateStaffCommand command)
    => HandleUpdateCommand(id, command);

  [HttpDelete("{id:length(26)}")]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<StaffDto>>> Delete(string id)
    => HandleDeleteCommand(new DeleteStaffCommand(id));
  [HttpGet("count-by-position")]
  [MapToApiVersion("1.0")]
  public async Task<ActionResult<ResultModel<IEnumerable<int>>>> GetCountByPosition()
  {
    return Ok(ResultModel<IEnumerable<int>>.Create(await Mediator.Send(new GetStaffCountByPositionQuery())));
  }
}
