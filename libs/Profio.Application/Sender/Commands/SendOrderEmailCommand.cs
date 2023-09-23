using FluentValidation;
using MediatR;
using Profio.Application.Sender.Validators;
using Profio.Domain.Constants;
using Profio.Domain.Contracts;
using Profio.Infrastructure.Email.FluentEmail;

namespace Profio.Application.Sender.Commands;

public sealed record SendOrderEmailCommand(OrderInfo OrderInfo, EmailType Type) : IRequest<Unit>;

public sealed class SendOrderEmailCommandHandler : IRequestHandler<SendOrderEmailCommand, Unit>
{
  private readonly IEmailService _emailService;

  public SendOrderEmailCommandHandler(IEmailService emailService)
    => _emailService = emailService;

  public async Task<Unit> Handle(SendOrderEmailCommand request, CancellationToken cancellationToken)
  {
    await _emailService.SendEmailAsync(new()
    {
      To = request.OrderInfo.Email,
      Subject = "Order Information",
      Template = $"{Directory.GetCurrentDirectory()}/wwwroot/Templates/Order.liquid",
      Model = new
      {
        Status = request.Type switch
        {
          EmailType.OrderPending => "Received the order",
          EmailType.OrderInProcess => "On delivery",
          EmailType.IncidentReported => "Has an incident",
          EmailType.OrderShipping => "Shipping",
          EmailType.CancelOrder => "Cancel",
          EmailType.OrderArrived => "Arrived the warehouse",
          EmailType.OrderCompleted => "Completed",
          _ => throw new ArgumentOutOfRangeException(nameof(request.Type), request.Type, "Invalid email type!"),
        },
        OrderDate = DateTime.UtcNow.ToString("dd/MM/yyyy"),
        request.OrderInfo.Id,
        request.OrderInfo.CustomerName,
        request.OrderInfo.Email,
        request.OrderInfo.Phone,
        request.OrderInfo.From,
        request.OrderInfo.To
      }
    });
    return Unit.Value;
  }
}
public sealed class SendOrderEmailCommandValidator : AbstractValidator<SendOrderEmailCommand>
{
  public SendOrderEmailCommandValidator(OrderInfoValidator orderInfoValidator)
    => RuleFor(x => x.OrderInfo)
      .SetValidator(orderInfoValidator);
}
