using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Events.Commands;
using Profio.Application.CQRS.Handlers.Command;
using Profio.Application.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Customers.Commands;

public record DeleteCustomerCommand(object Id) : DeleteCommandBase<CustomerDto>(Id);

public class DeleteCustomerCommandHandler : DeleteCommandHandlerBase<DeleteCustomerCommand, CustomerDto, Customer>
{
  public DeleteCustomerCommandHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class DeleteCustomerCommandValidator : DeleteCommandValidatorBase<DeleteCustomerCommand, CustomerDto>
{
}
