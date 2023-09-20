using FluentValidation;
using Profio.Domain.Models;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;

namespace Profio.Infrastructure.Abstractions.CQRS.Validators;

public class GetByIdQueryValidatorBase<TQuery, TModel> : AbstractValidator<TQuery>
  where TQuery : GetByIdQueryBase<TModel>
  where TModel : BaseModel
{
  public GetByIdQueryValidatorBase()
  {
    RuleFor(x => x.Id)
      .NotEmpty()
      .NotNull()
      .Must(x => x.ToString()!.Length == 26);
  }
}
