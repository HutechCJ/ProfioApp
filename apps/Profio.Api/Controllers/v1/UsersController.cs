using EntityFrameworkCore.Repository.Collections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.Users;
using Profio.Application.Users.Commands.ChangePassword;
using Profio.Application.Users.Commands.Login;
using Profio.Application.Users.Commands.Register;
using Profio.Application.Users.Queries;
using Profio.Domain.Models;
using Profio.Domain.Specifications;
using Profio.Infrastructure.Identity;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.Controllers.v1;

[ApiVersion("1.0")]
[Authorize]
[SwaggerTag("An authenticated and authorized user")]
public class UsersController : BaseEntityController<ApplicationUser, UserDto, GetUserByIdQuery>
{
  private readonly IUserAccessor _userAccessor;

  public UsersController(IUserAccessor userAccessor)
    => _userAccessor = userAccessor;
  [HttpPost("login")]
  [AllowAnonymous]
  [MapToApiVersion("1.0")]
  [SwaggerOperation(
    summary: "Login",
    description: "The API will return a token and cookie named `USER-TOKEN` to the user if the credentials are correct"
    )]
  public async Task<ActionResult<ResultModel<AccountDto>>> Login(LoginCommand loginCommand)
  {
    var result = await Mediator.Send(loginCommand);

    Response.Cookies.Append("USER-TOKEN", result.Token!, new()
    {
      HttpOnly = true,
      Expires = result.TokenExpire,
    });

    return Ok(ResultModel<AccountDto>.Create(result));
  }

  [HttpPost("register")]
  [AllowAnonymous]
  [MapToApiVersion("1.0")]
  public async Task<ActionResult<ResultModel<AccountDto>>> Register(RegisterCommand registerCommand)
    => Ok(ResultModel<AccountDto>.Create(await Mediator.Send(registerCommand)));

  [HttpPost("change-password")]
  public async Task<ActionResult<ResultModel<AccountDto>>> ChangePassword(ChangePasswordCommand changePasswordCommand)
    => Ok(ResultModel<AccountDto>.Create(await Mediator.Send(changePasswordCommand)));

  [HttpGet("{id:guid}")]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<UserDto>>> GetUserById(Guid id)
    => HandleGetByIdQuery(new GetUserByIdQuery(id));

  [HttpGet("check-authorization")]
  [MapToApiVersion("1.0")]
  public async Task<IActionResult> CheckAuthorization()
    => Ok(await Mediator.Send(new GetUserByIdQuery(_userAccessor.Id)));

  [HttpGet("get-users")]
  [AllowAnonymous]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<IPagedList<UserDto>>>> GetUsers([FromQuery] Criteria<ApplicationUser> criteria)
    => HandlePaginationQuery(new GetUserWithPagingQuery(criteria));
}
