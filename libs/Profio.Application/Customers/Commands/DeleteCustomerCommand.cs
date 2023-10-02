using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Events.Commands;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Command;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

namespace Profio.Application.Customers.Commands;

public sealed record DeleteCustomerCommand(object Id) : DeleteCommandBase<CustomerDto>(Id);

public sealed class
  DeleteCustomerCommandHandler : DeleteCommandHandlerBase<DeleteCustomerCommand, CustomerDto, Customer>
{
  public DeleteCustomerCommandHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class DeleteCustomerCommandValidator : DeleteCommandValidatorBase<DeleteCustomerCommand, CustomerDto>
{
}
