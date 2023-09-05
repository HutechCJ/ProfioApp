using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentValidation;
using Profio.Application.CQRS.Events.Commands;
using Profio.Application.CQRS.Handlers.Command;
using Profio.Application.CQRS.Validators;
using Profio.Domain.Constants;
using Profio.Domain.Entities;
using Profio.Domain.ValueObjects;

namespace Profio.Application.Customers.Commands;

public record UpdateCustomerCommand(object Id) : UpdateCommandBase(Id)
{
  public required string? Name { get; set; }
  public required string? Phone { get; set; }
  public string? Email { get; set; }
  public Gender? Gender { get; set; } = Domain.Constants.Gender.Male;
  public required Address? Address { get; set; }
};
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
    .NotEmpty()
    .NotNull();

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
