using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.Users.Commands.ChangePassword;
using Profio.Application.Users.Commands.Login;
using Profio.Application.Users.Commands.Register;
using Profio.Application.Users.Queries;
using Profio.Domain.Specifications;
using Profio.Infrastructure.Identity;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.Controllers.v1;

[ApiVersion("1.0")]
[Authorize]
[SwaggerTag("An authenticated and authorized user")]
public class UsersController : BaseController
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
  public async Task<IActionResult> Login(LoginCommand loginCommand)
  {
    var result = await Mediator.Send(loginCommand);

    Response.Cookies.Append("USER-TOKEN", result.Data!.Token!, new()
    {
      HttpOnly = true,
      Expires = result.Data.TokenExpire,
    });

    return Ok(result);
  }

  [HttpPost("register")]
  [AllowAnonymous]
  [MapToApiVersion("1.0")]
  public async Task<IActionResult> Register(RegisterCommand registerCommand)
    => Ok(await Mediator.Send(registerCommand));

  [HttpPost("change-password")]
  public async Task<IActionResult> ChangePassword(ChangePasswordCommand changePasswordCommand)
    => Ok(await Mediator.Send(changePasswordCommand));

  [HttpGet("{id:guid}")]
  [MapToApiVersion("1.0")]
  public async Task<IActionResult> GetUserById(Guid id)
    => Ok(await Mediator.Send(new GetUserByIdQuery(id)));

  [HttpGet("check-authorization")]
  [MapToApiVersion("1.0")]
  public async Task<IActionResult> CheckAuthorization()
    => Ok(await Mediator.Send(new GetUserByIdQuery(_userAccessor.Id)));

  [HttpGet("get-users")]
  [AllowAnonymous]
  [MapToApiVersion("1.0")]
  public async Task<IActionResult> GetUsers([FromQuery] Criteria<ApplicationUser> criteria)
    => Ok(await Mediator.Send(new GetUserWithPagingQuery(criteria)));
}
