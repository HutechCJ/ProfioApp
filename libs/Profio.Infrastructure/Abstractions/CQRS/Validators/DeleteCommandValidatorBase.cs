using FluentValidation;
using Profio.Domain.Models;
using Profio.Infrastructure.Abstractions.CQRS.Events.Commands;

namespace Profio.Infrastructure.Abstractions.CQRS.Validators;

public class DeleteCommandValidatorBase<TCommand, TModel> : AbstractValidator<TCommand>
  where TCommand : DeleteCommandBase<TModel>
  where TModel : BaseModel
{
  public DeleteCommandValidatorBase()
  {
    RuleFor(x => x.Id).NotEmpty().NotNull();
  }
}
