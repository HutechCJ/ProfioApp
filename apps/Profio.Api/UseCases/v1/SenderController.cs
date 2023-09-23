using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.Sender.Commands;
using Profio.Domain.Constants;
using Profio.Domain.Contracts;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.UseCases.v1;

[ApiVersion("1.0")]
[SwaggerTag("The Email Sender")]
[Authorize]
public sealed class SenderController : BaseController
{

  [HttpPost("email/order")]
  [SwaggerOperation(summary: "Send Order Email")]
  public async Task<IActionResult> SendEmail(OrderInfo order, [FromQuery] EmailType type)
  {
    await Mediator.Send(new SendOrderEmailCommand(order, type));
    return Ok("Send email successfully!");
  }
}
