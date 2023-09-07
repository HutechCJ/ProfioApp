using Profio.Application.CQRS;
using Profio.Application.Orders.Commands;
using Profio.Domain.Entities;

namespace Profio.Application.Orders;

public class OrderProfile : EntityProfileBase<Order, OrderDto, CreateOrderCommand, UpdateOrderCommand> { }
