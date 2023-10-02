using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentValidation;
using Profio.Domain.Constants;
using Profio.Domain.Entities;
using Profio.Domain.ValueObjects;
using Profio.Infrastructure.Abstractions.CQRS.Events.Commands;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Command;
using Profio.Infrastructure.Abstractions.CQRS.Validators;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Hubs.Commands;

[SwaggerSchema(
  Title = "Create Hub",
  Description = "A Representation of list of Hub")]
public sealed record CreateHubCommand : CreateCommandBase
{
  public required string Name { get; set; }
  public required string ZipCode { get; set; }
  public required Location Location { get; set; }
public Address? Address { get; set; }
public HubStatus Status { get; set; } = HubStatus.Active;
}

public sealed class CreateHubCommandHandler : CreateCommandHandlerBase<CreateHubCommand, Hub>
{
  public CreateHubCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class CreateHubCommandValidator : AbstractValidator<CreateHubCommand>
{
  public CreateHubCommandValidator()
  {
    RuleFor(h => h.ZipCode)
      .Length(10)
      .Matches("^[0-9]*$");

    RuleFor(c => c.Location)
      .SetValidator(new LocationValidator());

    RuleFor(c => c.Address)
      .SetValidator(new AddressValidator()!);
  }
}
