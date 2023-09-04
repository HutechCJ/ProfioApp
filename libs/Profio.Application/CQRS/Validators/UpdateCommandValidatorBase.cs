using FluentValidation;
using Profio.Application.CQRS.Events.Commands;

namespace Profio.Application.CQRS.Validators;

public class UpdateCommandValidatorBase<TCommand> : AbstractValidator<TCommand>
  where TCommand : UpdateCommandBase
{
  public UpdateCommandValidatorBase()
  {
    RuleFor(x => x.Id).NotEmpty().NotNull();
  }
}
