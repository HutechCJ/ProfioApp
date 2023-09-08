using FluentValidation;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Domain.Models;

namespace Profio.Application.Abstractions.CQRS.Validators;

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
