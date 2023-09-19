using Profio.Application.Abstractions.CQRS;
using Profio.Application.OrderHistories.Commands;
using Profio.Domain.Entities;

namespace Profio.Application.OrderHistories;

public sealed class OrderHistoryProfile : EntityProfileBase<OrderHistory, OrderHistoryDto, CreateOrderHistoryCommand, UpdateOrderHistoryCommand> { }
