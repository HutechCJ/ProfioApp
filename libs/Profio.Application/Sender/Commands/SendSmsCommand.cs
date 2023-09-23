using FluentValidation;
using MediatR;
using Profio.Application.Sender.Validators;
using Profio.Domain.Constants;
using Profio.Domain.Contracts;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;

namespace Profio.Application.Sender.Commands;

public sealed record SendSmsCommand(SmsMessage  Message, SmsType Type) : IRequest<Unit>;

public sealed class SendSmsCommandHandler : IRequestHandler<SendSmsCommand, Unit>
{
  private readonly ITwilioRestClient _client;

  public SendSmsCommandHandler(ITwilioRestClient client)
    => _client = client;

  public async Task<Unit> Handle(SendSmsCommand request, CancellationToken cancellationToken)
  {
    await MessageResource.CreateAsync(
      to: new(request.Message.To),
      from: new(request.Message.From),
      body: request.Type switch
      {
        SmsType.IncidentReported => $"Your order with id {request.Message.Message} has an incident",
        SmsType.OrderShipping => $"Your order with id {request.Message.Message} is shipping. Please keep your phone on",
        SmsType.OrderCompleted => $"Your order with id  {request.Message.Message} has been completed",
        SmsType.IncidentResolved => "An incident for your order has been resolved",
        _ => request.Message.Message
      },
      client: _client);

    return Unit.Value;
  }
}

public sealed class SendSmsCommandValidator : AbstractValidator<SendSmsCommand>
{
  public SendSmsCommandValidator(SmsMessageValidator smsMessageValidator)
    => RuleFor(x => x.Message)
      .SetValidator(smsMessageValidator);
}
