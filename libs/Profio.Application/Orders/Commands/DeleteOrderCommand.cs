using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Commands;
using Profio.Application.Abstractions.CQRS.Handlers.Command;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Orders.Commands;

public sealed record DeleteOrderCommand(object Id) : DeleteCommandBase<OrderDto>(Id);

public sealed class DeleteOrderCommandHandler : DeleteCommandHandlerBase<DeleteOrderCommand, OrderDto, Order>
{
  public DeleteOrderCommandHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class DeleteOrderCommandValidator : DeleteCommandValidatorBase<DeleteOrderCommand, OrderDto>
{
}
