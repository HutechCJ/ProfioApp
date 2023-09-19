using Profio.Domain.Constants;

namespace Profio.Application.Orders;

public sealed record OrderEnumFilter(OrderStatus? Status);
