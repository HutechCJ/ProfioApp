using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Events.Commands;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Command;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

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
