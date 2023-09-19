using Profio.Application.Abstractions.CQRS;
using Profio.Application.DeliveryProgresses.Commands;
using Profio.Domain.Entities;

namespace Profio.Application.DeliveryProgresses;

public sealed class DeliveryProgressProfile : EntityProfileBase<DeliveryProgress, DeliveryProgressDto, CreateDeliveryProgressCommand, UpdateDeliveryProgressCommand> { }
