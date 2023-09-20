using FluentValidation;
using Profio.Infrastructure.Abstractions.CQRS.Events.Commands;

namespace Profio.Infrastructure.Abstractions.CQRS.Validators;

public class UpdateCommandValidatorBase<TCommand> : AbstractValidator<TCommand>
  where TCommand : UpdateCommandBase
{
  public UpdateCommandValidatorBase()
  {
    RuleFor(x => x.Id).NotEmpty().NotNull();
  }
}
