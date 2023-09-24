using FluentValidation;
using Profio.Domain.Contracts;

namespace Profio.Application.Sender.Validators;

public class SmsMessageValidator : AbstractValidator<SmsMessage>
{
  public SmsMessageValidator()
  {
    RuleFor(x => x.To)
      .NotEmpty()
      .Length(10)
      .Matches("^[0-9]*$");

    RuleFor(x => x.Message)
      .MaximumLength(50)
      .NotEmpty();
  }
}
