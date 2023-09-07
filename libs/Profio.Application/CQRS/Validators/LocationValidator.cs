using FluentValidation;
using Profio.Domain.ValueObjects;

namespace Profio.Application.CQRS.Validators;

public class LocationValidator : AbstractValidator<Location>
{
  public LocationValidator()
  {
    RuleFor(l => l.Latitude)
      .NotEmpty()
      .NotNull();

    RuleFor(l => l.Longitude)
      .NotEmpty()
      .NotNull();
  }
}
