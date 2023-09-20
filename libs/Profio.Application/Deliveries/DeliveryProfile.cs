using Profio.Application.Deliveries.Commands;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS;

namespace Profio.Application.Deliveries;

public sealed class DeliveryProfile : EntityProfileBase<Delivery, DeliveryDto, CreateDeliveryCommand, UpdateDeliveryCommand> { }
