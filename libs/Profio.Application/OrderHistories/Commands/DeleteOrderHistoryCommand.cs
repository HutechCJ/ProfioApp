using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Commands;
using Profio.Application.Abstractions.CQRS.Handlers.Command;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.OrderHistories.Commands;

public record DeleteOrderHistoryCommand(object Id) : DeleteCommandBase<OrderHistoryDto>(Id);

public class DeleteOrderHistoryCommandHandler : DeleteCommandHandlerBase<DeleteOrderHistoryCommand, OrderHistoryDto, OrderHistory>
{
  public DeleteOrderHistoryCommandHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class DeleteOrderHistoryCommandValidator : DeleteCommandValidatorBase<DeleteOrderHistoryCommand, OrderHistoryDto>
{
}
