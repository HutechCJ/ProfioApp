using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Constants;
using Profio.Domain.Entities;
using Profio.Domain.ValueObjects;
using Profio.Infrastructure.Abstractions.CQRS.Events.Commands;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Command;
using Profio.Infrastructure.Abstractions.CQRS.Validators;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Hubs.Commands;

[SwaggerSchema(
  Title = "Update Hub",
  Description = "A Representation of Hub")]
public sealed record UpdateHubCommand(object Id) : UpdateCommandBase(Id)
{
  public string? Name { get; set; }
  public string? ZipCode { get; set; }
  public Location? Location { get; set; }
  public Address? Address { get; set; }
  public HubStatus? Status { get; set; }
}

public sealed class UpdateHubCommandHandler : UpdateCommandHandlerBase<UpdateHubCommand, Hub>
{
  public UpdateHubCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class UpdateHubCommandValidator : UpdateCommandValidatorBase<UpdateHubCommand>
{
  public UpdateHubCommandValidator()
  {
    RuleFor(c => c.Name);

    RuleFor(c => c.Address)
      .SetValidator(new AddressValidator()!);
  }
}
