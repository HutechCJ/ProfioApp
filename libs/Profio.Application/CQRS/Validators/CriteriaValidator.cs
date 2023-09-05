using FluentValidation;
using Profio.Domain.Interfaces;
using Profio.Domain.Specifications;

namespace Profio.Application.CQRS.Validators;

public class CriteriaValidator<TEntity> : AbstractValidator<Criteria<TEntity>>
  where TEntity : class, IEntity<object>
{
  public CriteriaValidator()
  {
    RuleFor(x => x.PageNumber)
          .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

    RuleFor(x => x.PageSize)
    .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");

    RuleFor(x => x.Filter)
        .MaximumLength(100);
  }
}
