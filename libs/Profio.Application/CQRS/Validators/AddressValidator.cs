using FluentValidation;
using Profio.Domain.ValueObjects;

namespace Profio.Application.CQRS.Validators;

public class AddressValidator : AbstractValidator<Address>
{
  public AddressValidator()
  {
    RuleFor(a => a.ZipCode)
      .Matches("^[0-9]*$");

    RuleFor(a => a.City);

    RuleFor(a => a.Street);

    RuleFor(a => a.Ward);

    RuleFor(a => a.Province);
  }
}
