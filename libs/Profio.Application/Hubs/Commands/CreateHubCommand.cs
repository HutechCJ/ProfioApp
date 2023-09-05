using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentValidation;
using Profio.Application.CQRS.Events.Commands;
using Profio.Application.CQRS.Handlers.Command;
using Profio.Application.CQRS.Validators;
using Profio.Domain.Constants;
using Profio.Domain.Entities;
using Profio.Domain.ValueObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Hubs.Commands;

[SwaggerSchema(
  Title = "Hub Request",
  Description = "A Representation of list of Hub")]
public record CreateHubCommand : CreateCommandBase
{
    public required string? Name { get; set; }
    public required string? ZipCode { get; set; }
    public Location? Location { get; set; }
    public Address? Address { get; set; }
    public HubStatus Status { get; set; } = HubStatus.Active;
}

public class CreateHubCommandHandler : CreateCommandHandlerBase<CreateHubCommand, Hub>
{
    public CreateHubCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}

public class CreateHubCommandValidator : AbstractValidator<CreateHubCommand>
{
    public CreateHubCommandValidator()
    {
        RuleFor(c => c.Location)
          .NotEmpty()
          .NotNull();

        RuleFor(c => c.Address)
          .SetValidator(new AddressValidator()!);
    }
}
