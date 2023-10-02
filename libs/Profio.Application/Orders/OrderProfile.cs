using Profio.Application.Orders.Commands;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS;

namespace Profio.Application.Orders;

public sealed class OrderProfile : EntityProfileBase<Order, OrderDto, CreateOrderCommand, UpdateOrderCommand>
{
}
