using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profio.Domain.Constants;
using Profio.Domain.Contracts;
using Profio.Infrastructure.Email.FluentEmail;
using Profio.Infrastructure.Message;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.UseCases.v1;

[ApiVersion("1.0")]
[SwaggerTag("The Email Sender")]
[Authorize]
public class SenderController : BaseController
{
  private readonly IEmailService _emailService;

  public SenderController(IEmailService emailService)
    => _emailService = emailService;

  [HttpPost("email/order")]
  [MapToApiVersion("1.0")]
  public async Task<IActionResult> SendEmail(OrderInfo order, [FromQuery] EmailType type)
  {
    await _emailService.SendEmailAsync(new()
    {
      To = order.Email,
      Subject = "Order Information",
      Template = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development"
      ? $"{Directory.GetCurrentDirectory()}/Templates/Order.liquid"
      : $"{Directory.GetCurrentDirectory()}/wwwroot/Templates/Order.liquid",
      Model = new
      {
        Status = type switch
        {
          EmailType.OrderPending => "Received the order",
          EmailType.OrderInProcess => "On delivery",
          EmailType.IncidentReported => "Has an incident",
          EmailType.OrderShipping => "Shipping",
          EmailType.CancelOrder => "Cancel",
          EmailType.OrderArrived => "Arrived the warehouse",
          EmailType.OrderCompleted => "Completed",
          _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Invalid email type!"),
        },
        OrderDate = DateTime.UtcNow.ToString("dd/MM/yyyy"),
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
}
