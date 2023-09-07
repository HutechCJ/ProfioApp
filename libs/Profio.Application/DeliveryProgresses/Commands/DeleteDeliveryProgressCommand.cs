using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Events.Commands;
using Profio.Application.CQRS.Handlers.Command;
using Profio.Application.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.DeliveryProgresses.Commands;

public record DeleteDeliveryProgressCommand(object Id) : DeleteCommandBase<DeliveryProgressDto>(Id);

public class DeleteDeliveryProgressCommandHandler : DeleteCommandHandlerBase<DeleteDeliveryProgressCommand, DeliveryProgressDto, DeliveryProgress>
{
  public DeleteDeliveryProgressCommandHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class DeleteDeliveryProgressCommandValidator : DeleteCommandValidatorBase<DeleteDeliveryProgressCommand, DeliveryProgressDto>
{
}
