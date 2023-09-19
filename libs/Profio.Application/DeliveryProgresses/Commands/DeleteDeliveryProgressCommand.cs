using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Commands;
using Profio.Application.Abstractions.CQRS.Handlers.Command;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.DeliveryProgresses.Commands;

public sealed record DeleteDeliveryProgressCommand(object Id) : DeleteCommandBase<DeliveryProgressDto>(Id);

public sealed class DeleteDeliveryProgressCommandHandler : DeleteCommandHandlerBase<DeleteDeliveryProgressCommand, DeliveryProgressDto, DeliveryProgress>
{
  public DeleteDeliveryProgressCommandHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class DeleteDeliveryProgressCommandValidator : DeleteCommandValidatorBase<DeleteDeliveryProgressCommand, DeliveryProgressDto>
{
}
