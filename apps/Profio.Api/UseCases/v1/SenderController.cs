using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profio.Domain.Constants;
using Profio.Domain.Contracts;
using Profio.Infrastructure.Email.FluentEmail;
using Profio.Infrastructure.Key;
using Profio.Infrastructure.Message;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.UseCases.v1;

[ApiVersion("1.0")]
[SwaggerTag("The Email Sender")]
[Authorize]
public class SenderController : BaseController
{
  private readonly IEmailService _emailService;
  private readonly IMessageService _messageService;

  public SenderController(IEmailService emailService, IMessageService messageService)
    => (_emailService, _messageService) = (emailService, messageService);

  [HttpPost("email/order-info")]
  [MapToApiVersion("1.0")]
  public async Task<IActionResult> SendEmail(OrderInfo order)
  {
    await _emailService.SendEmailAsync(new()
    {
      To = order.Email,
      Subject = "Order Information",
      Template = $"{Directory.GetCurrentDirectory()}/Templates/OrderInfo.liquid",
      Model = new
      {
        OrderDate = DateTime.Now.ToString("dd/MM/yyyy"),
        order.Id,
        order.CustomerName,
        order.Email,
        order.Phone,
        order.From,
        order.To
      }
    });

    return Ok("Send email successfully!");
  }

  [HttpGet("sms/order-info/{phone:length(10)}")]
  [MapToApiVersion("1.0")]
  [ApiKey]
  public async Task<IActionResult> SendSms(string phone, [FromQuery] MessageType type)
  {
    var message = type switch
    {
      MessageType.OrderReceived => "Cam on ban da su dung dich vu cua chung toi CJ Logistics. Chung toi se chuyen hang den ban trong thoi gian som nhat",
      MessageType.OrderShipped => "Van don cua ban da duoc chuyen di. Vui long kiem tra lai thong tin van don cua ban",
      MessageType.IncidentReported => "Van don cua ban da gap su co. Chung toi se giai quyet trong thoi gian som nhat",
      MessageType.IncidentResolved => "Su co ve van don cua ban da duoc giai quyet",
      MessageType.CancelOrder => "Van don cua ban da bi huy. Vui long lien he voi chung toi de biet them chi tiet",
      _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Invalid message type!"),
    };

    await _messageService.SendSms(phone, message);
    return Ok("Send sms successfully!");
  }
}
