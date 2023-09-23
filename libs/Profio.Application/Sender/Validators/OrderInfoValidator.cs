using FluentValidation;
using Profio.Domain.Contracts;

namespace Profio.Application.Sender.Validators;

public class OrderInfoValidator : AbstractValidator<OrderInfo>
{
  public OrderInfoValidator()
  {
    RuleFor(x => x.Phone)
      .Length(10)
      .Matches("^[0-9]*$");

    RuleFor(x => x.Email)
      .EmailAddress();
  }
}
