using Profio.Domain.Constants;

namespace Profio.Application.Orders;

public record OrderEnumFilter(OrderStatus? Status);
