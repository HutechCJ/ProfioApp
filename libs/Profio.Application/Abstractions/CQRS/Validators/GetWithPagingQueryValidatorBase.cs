using FluentValidation;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Domain.Models;

namespace Profio.Application.Abstractions.CQRS.Validators;

public class GetWithPagingQueryValidatorBase<TQuery, TModel> : AbstractValidator<TQuery>
  where TQuery : GetWithPagingQueryBase<TModel>
  where TModel : BaseModel
{
  public GetWithPagingQueryValidatorBase()
  {
    RuleFor(q => q.Criteria).SetValidator(new CriteriaValidator());
  }
}
