using Profio.Application.Abstractions.CQRS;
using Profio.Application.Orders.Commands;
using Profio.Domain.Entities;

namespace Profio.Application.Orders;

public sealed class OrderProfile : EntityProfileBase<Order, OrderDto, CreateOrderCommand, UpdateOrderCommand> { }
