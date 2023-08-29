using MediatR;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.Users.Commands.Login;
using Profio.Application.Users.Commands.Register;
using Profio.Application.Users.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
[SwaggerTag("An authenticated and authorized user")]
public class UsersController : ControllerBase
{
  private readonly IMediator _mediator;

  public UsersController(IMediator mediator)
    => _mediator = mediator;

  [HttpPost("login")]
  public async Task<IActionResult> Login(LoginCommand loginCommand)
  {
    var result = await _mediator.Send(loginCommand);
    if (result.IsError)
      return Unauthorized(result);
    return Ok(result);
  }


  [HttpPost("register")]
  public async Task<IActionResult> Register(RegisterCommand registerCommand)
  {
    var result = await _mediator.Send(registerCommand);
    if (result.IsError)
      return BadRequest(result);
    return Ok(result);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetUserById(string id)
  {
    var result = await _mediator.Send(new GetUserByIdQuery(id));
    if (result.IsError)
      return BadRequest();
    return Ok(result);
  }
}
