using FluentValidation;
using Profio.Domain.Specifications;

namespace Profio.Infrastructure.Abstractions.CQRS.Validators;

public class CriteriaValidator : AbstractValidator<Criteria>
{
  public CriteriaValidator()
  {
    RuleFor(x => x.PageIndex)
      .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

    RuleFor(x => x.PageSize)
      .GreaterThanOrEqualTo(0).WithMessage("PageSize at least greater than or equal to 0.");

    RuleFor(x => x.Filter)
      .MaximumLength(100);
  }
}
