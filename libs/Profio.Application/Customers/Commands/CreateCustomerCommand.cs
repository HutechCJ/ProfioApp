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
  Title = "Customer Create Request",
  Description = "A Representation of list of Customer")]
public record CreateCustomerCommand : CreateCommandBase
{
  public required string? Name { get; set; }
  public required string? Phone { get; set; }
  public string? Email { get; set; }
  public Gender? Gender { get; set; } = Domain.Constants.Gender.Male;
  public required Address? Address { get; set; }
}

public class CreateCustomerCommandHandler : CreateCommandHandlerBase<CreateCustomerCommand, Customer>
{
  public CreateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
  public CreateCustomerCommandValidator()
  {
    RuleFor(c => c.Name)
      .NotEmpty()
      .NotNull()
      .MaximumLength(50);

    RuleFor(c => c.Phone)
      .Length(10)
      .Matches("^[0-9]*$");

    RuleFor(c => c.Email)
      .EmailAddress()
      .MaximumLength(50);

    RuleFor(c => c.Gender)
      .IsInEnum();

    RuleFor(c => c.Address)
      .SetValidator(new AddressValidator()!);
  }
}
