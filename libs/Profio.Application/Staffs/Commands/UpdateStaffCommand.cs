using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentValidation;
using Profio.Application.Abstractions.CQRS.Events.Commands;
using Profio.Application.Abstractions.CQRS.Handlers.Command;
using Profio.Domain.Constants;
using Profio.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Staffs.Commands;

[SwaggerSchema(
  Title = "Staff Update Request",
  Description = "A Representation of list of Staff")]
public sealed record UpdateStaffCommand(object Id) : UpdateCommandBase(Id)
{
  public string? Name { get; set; }
  public string? Phone { get; set; }
  public Position? Position { get; set; }
}

public sealed class UpdateStaffCommandHandler : UpdateCommandHandlerBase<UpdateStaffCommand, Staff>
{
  public UpdateStaffCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class UpdateStaffCommandValidator : AbstractValidator<UpdateStaffCommand>
{
  public UpdateStaffCommandValidator()
  {
    RuleFor(s => s.Name)
      .MaximumLength(50);

    RuleFor(s => s.Phone)
      .Length(10)
      .Matches("^[0-9]*$");

    RuleFor(s => s.Position)
      .IsInEnum();
  }
}
