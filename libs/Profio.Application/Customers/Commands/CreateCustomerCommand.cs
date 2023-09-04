using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Events.Commands;
using Profio.Application.CQRS.Handlers.Command;
using Profio.Domain.Constants;
using Profio.Domain.Entities;
using Profio.Domain.ValueObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Customers.Commands;

[SwaggerSchema(
  Title = "Customer Request",
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
