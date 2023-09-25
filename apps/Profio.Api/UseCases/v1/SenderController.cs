using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.Sender.Commands;
using Profio.Domain.Constants;
using Profio.Domain.Contracts;
using Profio.Domain.Models;
using Profio.Infrastructure.Key;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.UseCases.v1;

[ApiVersion("1.0")]
[SwaggerTag("The Email Sender")]
[Authorize]
public sealed class SenderController : BaseController
{
  [HttpPost("email/order")]
  [SwaggerOperation(summary: "Send Order Email")]
  public async Task<ActionResult<ResultModel<string>>> SendEmail(OrderInfo order, [FromQuery] EmailType type)
  {
    await Mediator.Send(new SendOrderEmailCommand(order, type));
    return Ok(ResultModel<string>.Create("Send email successfully!"));
  }

  [HttpPost("sms")]
  [SwaggerOperation(
    summary: "Send SMS",
    description: "If you choose the type of sms, the message must be the `Order Id`. Otherwise, you can send any message you want")]
  [ApiKey]
  public async Task<ActionResult<ResultModel<string>>> SendSms(SmsMessage message, [FromQuery] SmsType type)
  {
    await Mediator.Send(new SendSmsCommand(message, type));
    return Ok(ResultModel<string>.Create("Send sms successfully!"));
  }
}
