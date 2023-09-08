using FluentValidation;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Domain.Interfaces;
using Profio.Domain.Models;

namespace Profio.Application.Abstractions.CQRS.Validators;

public class GetWithPagingQueryValidatorBase<TEntity, TQuery, TModel> : AbstractValidator<TQuery>
  where TQuery : GetWithPagingQueryBase<TEntity, TModel>
  where TEntity : class, IEntity<object>
  where TModel : BaseModel
{
  public GetWithPagingQueryValidatorBase()
  {
    RuleFor(q => q.Criteria).SetValidator(new CriteriaValidator<TEntity>());
  }
}
