using FluentValidation;
using Profio.Application.CQRS.Events.Commands;
using Profio.Domain.Models;

namespace Profio.Application.CQRS.Validators;

public class DeleteCommandValidatorBase<TCommand, TModel> : AbstractValidator<TCommand>
  where TCommand : DeleteCommandBase<TModel>
  where TModel : BaseModel
{
  public DeleteCommandValidatorBase()
  {
    RuleFor(x => x.Id).NotEmpty().NotNull();
  }
}
