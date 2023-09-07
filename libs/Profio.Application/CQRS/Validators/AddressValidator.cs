using FluentValidation;
using Profio.Domain.ValueObjects;

namespace Profio.Application.CQRS.Validators;

public class AddressValidator : AbstractValidator<Address>
{
  public AddressValidator()
  {
    RuleFor(a => a.ZipCode)
      .Matches("^[0-9]*$")
      .MaximumLength(10);

    RuleFor(a => a.City)
      .MaximumLength(50);

    RuleFor(a => a.Street)
      .MaximumLength(50);

    RuleFor(a => a.Ward)
      .MaximumLength(50);

    RuleFor(a => a.Province)
      .MaximumLength(50);
  }
}
