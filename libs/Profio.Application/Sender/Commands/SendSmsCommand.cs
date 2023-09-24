using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Profio.Application.Sender.Validators;
using Profio.Domain.Constants;
using Profio.Domain.Contracts;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;

namespace Profio.Application.Sender.Commands;

public sealed record SendSmsCommand(SmsMessage Message, SmsType Type) : IRequest<Unit>;

public sealed class SendSmsCommandHandler : IRequestHandler<SendSmsCommand, Unit>
{
  private readonly ITwilioRestClient _client;
  private readonly IConfiguration _configuration;

  public SendSmsCommandHandler(ITwilioRestClient client, IConfiguration configuration)
  {
    _client = client;
    _configuration = configuration;
  }

  public async Task<Unit> Handle(SendSmsCommand request, CancellationToken cancellationToken)
  {
    var validPhoneNumber = $"+84{request.Message.To?[1..]}";
    await MessageResource.CreateAsync(
      to: new(validPhoneNumber),
      from: new(_configuration["Twilio:FromPhoneNumber"]),
      body: request.Type switch
      {
        SmsType.IncidentReported => $"Your order with id {request.Message.Message} has an incident",
        SmsType.OrderShipping => $"Your order with id {request.Message.Message} is shipping. Please keep your phone on",
        SmsType.OrderCompleted => $"Your order with id  {request.Message.Message} has been completed",
        SmsType.IncidentResolved => $"An incident of your order with id {request.Message.Message} has been resolved",
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
