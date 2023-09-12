using FluentValidation;
using Profio.Application.Abstractions.CQRS.Events.Commands;

namespace Profio.Application.Abstractions.CQRS.Validators;

public class UpdateCommandValidatorBase<TCommand> : AbstractValidator<TCommand>
  where TCommand : UpdateCommandBase
{
  public UpdateCommandValidatorBase()
  {
    RuleFor(x => x.Id).NotEmpty().NotNull();
  }
}
