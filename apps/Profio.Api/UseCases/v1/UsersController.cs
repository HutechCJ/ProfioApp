using EntityFrameworkCore.Repository.Collections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.Users;
using Profio.Application.Users.Commands.ChangePassword;
using Profio.Application.Users.Commands.Login;
using Profio.Application.Users.Commands.Register;
using Profio.Application.Users.Queries;
using Profio.Domain.Constants;
using Profio.Domain.Identity;
using Profio.Domain.Models;
using Profio.Domain.Specifications;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.UseCases.v1;

[ApiVersion("1.0")]
[Authorize]
[SwaggerTag("An authenticated and authorized user")]
public class UsersController : BaseEntityController<ApplicationUser, UserDto, GetUserByIdQuery>
{
  [HttpPost("login")]
  [AllowAnonymous]
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
  public async Task<ActionResult<ResultModel<AccountDto>>> Register(RegisterCommand registerCommand)
    => Ok(ResultModel<AccountDto>.Create(await Mediator.Send(registerCommand)));

  [HttpPost("change-password")]
  [SwaggerOperation(summary: "Change Password")]
  public async Task<ActionResult<ResultModel<AccountDto>>> ChangePassword(ChangePasswordCommand changePasswordCommand)
    => Ok(ResultModel<AccountDto>.Create(await Mediator.Send(changePasswordCommand)));

  [HttpGet("{id:guid}")]
  [SwaggerOperation(summary: "Get User by Id")]
  public Task<ActionResult<ResultModel<UserDto>>> GetUserById(Guid id)
    => HandleGetByIdQuery(new(id));

  [HttpGet("check-authorization")]
  [SwaggerOperation(summary: "Check user's autherization status")]
  public async Task<ActionResult<ResultModel<AccountDto>>> CheckAuthorization()
    => Ok(ResultModel<AccountDto>.Create(await Mediator.Send(new CheckAuthorizationQuery())));

  [HttpGet("get-users")]
  [SwaggerOperation(summary: "Get User list")]
  [Authorize(Roles = UserRole.Administrator)]
  public Task<ActionResult<ResultModel<IPagedList<UserDto>>>> GetUsers([FromQuery] Criteria criteria)
    => HandlePaginationQuery(new GetUserWithPagingQuery(criteria));
}
