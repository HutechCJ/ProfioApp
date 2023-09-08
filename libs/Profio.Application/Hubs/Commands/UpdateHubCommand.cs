using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Commands;
using Profio.Application.Abstractions.CQRS.Handlers.Command;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Constants;
using Profio.Domain.Entities;
using Profio.Domain.ValueObjects;

namespace Profio.Application.Hubs.Commands;

public record UpdateHubCommand(object Id) : UpdateCommandBase(Id)
{
  public required string? Name { get; set; }
  public required string? ZipCode { get; set; }
  public Location? Location { get; set; }
  public Address? Address { get; set; }
  public HubStatus? Status { get; set; }
}

public class UpdateHubCommandHandler : UpdateCommandHandlerBase<UpdateHubCommand, Hub>
{
  public UpdateHubCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class UpdateHubCommandValidator : UpdateCommandValidatorBase<UpdateHubCommand>
{
  public UpdateHubCommandValidator()
  {
    RuleFor(c => c.Name);

    RuleFor(c => c.Address)
      .SetValidator(new AddressValidator()!);
  }
}
