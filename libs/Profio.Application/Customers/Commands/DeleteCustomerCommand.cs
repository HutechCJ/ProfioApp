using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Commands;
using Profio.Application.Abstractions.CQRS.Handlers.Command;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Customers.Commands;

public sealed record DeleteCustomerCommand(object Id) : DeleteCommandBase<CustomerDto>(Id);

public sealed class DeleteCustomerCommandHandler : DeleteCommandHandlerBase<DeleteCustomerCommand, CustomerDto, Customer>
{
  public DeleteCustomerCommandHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class DeleteCustomerCommandValidator : DeleteCommandValidatorBase<DeleteCustomerCommand, CustomerDto>
{
}
