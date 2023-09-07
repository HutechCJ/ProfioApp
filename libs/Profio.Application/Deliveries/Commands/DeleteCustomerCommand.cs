using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Events.Commands;
using Profio.Application.CQRS.Handlers.Command;
using Profio.Application.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Deliveries.Commands;

public record DeleteDeliveryCommand(object Id) : DeleteCommandBase<DeliveryDto>(Id);

public class DeleteDeliveryCommandHandler : DeleteCommandHandlerBase<DeleteDeliveryCommand, DeliveryDto, Delivery>
{
  public DeleteDeliveryCommandHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class DeleteDeliveryCommandValidator : DeleteCommandValidatorBase<DeleteDeliveryCommand, DeliveryDto>
{
}
