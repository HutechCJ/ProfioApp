using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Events.Commands;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Command;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

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
