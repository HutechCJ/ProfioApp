using Profio.Application.DeliveryProgresses.Commands;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS;

namespace Profio.Application.DeliveryProgresses;

public sealed class DeliveryProgressProfile : EntityProfileBase<DeliveryProgress, DeliveryProgressDto,
  CreateDeliveryProgressCommand, UpdateDeliveryProgressCommand>
{
}
