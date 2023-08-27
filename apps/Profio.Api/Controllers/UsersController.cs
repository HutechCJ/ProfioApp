using MediatR;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.Users.Commands.Login;

namespace Profio.Api.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
  private readonly IMediator _mediator;

  public UsersController(IMediator mediator)
    => _mediator = mediator;

  [HttpPost("login")]
  public async Task<IActionResult> Login(string userName, string password)
  {
    await _mediator.Send(new LoginCommand(userName, password));
    return Ok();
  }
}
