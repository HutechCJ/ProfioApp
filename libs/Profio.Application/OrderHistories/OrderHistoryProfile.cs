using Profio.Application.Abstractions.CQRS;
using Profio.Application.OrderHistories.Commands;
using Profio.Domain.Entities;

namespace Profio.Application.OrderHistories;

public class OrderHistoryProfile : EntityProfileBase<OrderHistory, OrderHistoryDto, CreateOrderHistoryCommand, UpdateOrderHistoryCommand> { }
