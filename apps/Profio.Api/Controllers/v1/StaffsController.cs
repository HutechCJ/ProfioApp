using Microsoft.AspNetCore.Mvc;
using Profio.Application.Staffs;
using Profio.Application.Staffs.Commands;
using Profio.Domain.Entities;
using Profio.Domain.Models;

namespace Profio.Api.Controllers.v1;
[ApiVersion("1.0")]
public class StaffsController : BaseEntityController<Staff, StaffDto>
{
  [HttpPost]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<object>>> Post(CreateStaffCommand command)
    => HandleCreateCommand(command);
}
