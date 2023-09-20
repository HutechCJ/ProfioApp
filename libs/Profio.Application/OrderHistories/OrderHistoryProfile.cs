using Profio.Application.OrderHistories.Commands;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS;

namespace Profio.Application.OrderHistories;

public sealed class OrderHistoryProfile : EntityProfileBase<OrderHistory, OrderHistoryDto, CreateOrderHistoryCommand, UpdateOrderHistoryCommand> { }
