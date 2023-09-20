using FluentValidation;
using Profio.Domain.Models;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;

namespace Profio.Infrastructure.Abstractions.CQRS.Validators;

public class GetWithPagingQueryValidatorBase<TQuery, TModel> : AbstractValidator<TQuery>
  where TQuery : GetWithPagingQueryBase<TModel>
  where TModel : BaseModel
{
  public GetWithPagingQueryValidatorBase()
  {
    RuleFor(q => q.Criteria).SetValidator(new CriteriaValidator());
  }
}
