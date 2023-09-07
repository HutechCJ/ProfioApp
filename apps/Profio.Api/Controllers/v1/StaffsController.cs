using Microsoft.AspNetCore.Mvc;
using Profio.Application.Staffs;
using Profio.Application.Staffs.Commands;
using Profio.Application.Staffs.Queries;
using Profio.Domain.Entities;
using Profio.Domain.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.Controllers.v1;

[ApiVersion("1.0")]
[SwaggerTag("Manage staffs")]
public class StaffsController : BaseEntityController<Staff, StaffDto, GetStaffByIdQuery>
{
  [HttpPost]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<StaffDto>>> Post(CreateStaffCommand command)
    => HandleCreateCommand(command);
}
