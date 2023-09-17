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
  [SwaggerOperation(summary: "Get Staff List with Paging")]
  public Task<ActionResult<ResultModel<IPagedList<StaffDto>>>> Get([FromQuery] Criteria criteria, [FromQuery] StaffEnumFilter staffEnumFilter)
    => HandlePaginationQuery(new GetStaffWithPagingQuery(criteria, staffEnumFilter));

  [HttpGet("{id:length(26)}")]
  [SwaggerOperation(summary: "Get Staff by Id")]
  public Task<ActionResult<ResultModel<StaffDto>>> GetById(string id)
    => HandleGetByIdQuery(new(id));

  [HttpPost]
  [SwaggerOperation(summary: "Create Staff")]
  public Task<ActionResult<ResultModel<StaffDto>>> Post(CreateStaffCommand command)
    => HandleCreateCommand(command);

  [HttpPut("{id:length(26)}")]
  [SwaggerOperation(summary: "Update Staff")]
  public Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdateStaffCommand command)
    => HandleUpdateCommand(id, command);

  [HttpDelete("{id:length(26)}")]
  [SwaggerOperation(summary: "Delete Staff")]
  public Task<ActionResult<ResultModel<StaffDto>>> Delete(string id)
    => HandleDeleteCommand(new DeleteStaffCommand(id));
  [HttpGet("count-by-position")]
  [SwaggerOperation(summary: "Count Staff by Position")]
  public async Task<ActionResult<ResultModel<IEnumerable<int>>>> GetCountByPosition()
    => Ok(ResultModel<IEnumerable<int>>.Create(await Mediator.Send(new GetStaffCountByPositionQuery())));
  [HttpGet("count")]
  [SwaggerOperation(summary: "Get Staff count")]
  public async Task<ActionResult<ResultModel<int>>> GetCount()
    => Ok(ResultModel<int>.Create(await Mediator.Send(new GetStaffCountQuery())));
}
