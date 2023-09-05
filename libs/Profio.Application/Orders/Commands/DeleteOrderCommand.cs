using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Events.Commands;
using Profio.Application.CQRS.Handlers.Command;
using Profio.Application.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Orders.Commands;

public record DeleteOrderCommand(object Id) : DeleteCommandBase<OrderDto>(Id);
public class DeleteOrderCommandHandler : DeleteCommandHandlerBase<DeleteOrderCommand, OrderDto, Order>
{
  public DeleteOrderCommandHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}
public class DeleteOrderCommandValidator : DeleteCommandValidatorBase<DeleteOrderCommand, OrderDto> { }
