using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
  [HttpPost("login")]
  [AllowAnonymous]
  [MapToApiVersion("1.0")]
  public async Task<IActionResult> Login(LoginCommand loginCommand)
  {
    var result = await Mediator.Send(loginCommand);
    if (result.IsError)
      return Unauthorized(result);

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
  {
    var result = await Mediator.Send(registerCommand);
    if (result.IsError)
      return BadRequest(result);
    return Ok(result);
  }

  [HttpGet("{id:guid}")]
  [MapToApiVersion("1.0")]
  public async Task<IActionResult> GetUserById(Guid id)
  {
    var result = await Mediator.Send(new GetUserByIdQuery(id));
    if (result.IsError)
      return BadRequest();
    return Ok(result);
  }

  [HttpGet("check-authorization")]
  [MapToApiVersion("1.0")]
  public IActionResult CheckAuthorization() => Ok();

  [HttpGet("get-users")]
  [AllowAnonymous]
  [MapToApiVersion("1.0")]
  public async Task<IActionResult> GetUsers([FromQuery] Criteria<ApplicationUser> criteria)
    => Ok(await Mediator.Send(new GetUserWithPagingQuery(criteria)));
}
