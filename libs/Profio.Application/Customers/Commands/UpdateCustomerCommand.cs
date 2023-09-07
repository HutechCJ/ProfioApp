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

namespace Profio.Application.Customers.Commands;

[SwaggerSchema(
  Title = "Customer Update Request",
  Description = "A Representation of list of Customer")]
public record UpdateCustomerCommand(object Id) : UpdateCommandBase(Id)
{
  public required string? Name { get; set; }
  public required string? Phone { get; set; }
  public string? Email { get; set; }
  public Gender? Gender { get; set; }
  public required Address? Address { get; set; }
}

public class UpdateCustomerCommandHandler : UpdateCommandHandlerBase<UpdateCustomerCommand, Customer>
{
  public UpdateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class UpdateCustomerCommandValidator : UpdateCommandValidatorBase<UpdateCustomerCommand>
{
  public UpdateCustomerCommandValidator()
  {
    RuleFor(c => c.Name)
      .MaximumLength(50);

    RuleFor(c => c.Phone)
      .Length(10)
      .Matches("^[0-9]*$");

    RuleFor(c => c.Email)
      .EmailAddress();

    RuleFor(c => c.Gender)
      .IsInEnum();

    RuleFor(c => c.Address)
      .SetValidator(new AddressValidator()!);
  }
}
