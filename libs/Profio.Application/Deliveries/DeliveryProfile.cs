using Profio.Application.Abstractions.CQRS;
using Profio.Application.Deliveries.Commands;
using Profio.Domain.Entities;

namespace Profio.Application.Deliveries;

public sealed class DeliveryProfile : EntityProfileBase<Delivery, DeliveryDto, CreateDeliveryCommand, UpdateDeliveryCommand> { }
