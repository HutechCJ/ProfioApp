using Profio.Application.CQRS;
using Profio.Application.Deliveries.Commands;
using Profio.Domain.Entities;

namespace Profio.Application.Deliveries;

public class DeliveryProfile : EntityProfileBase<Delivery, DeliveryDto, CreateDeliveryCommand, UpdateDeliveryCommand> { }
