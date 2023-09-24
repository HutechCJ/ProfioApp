using Profio.Website.Data.Common;
using Profio.Website.Data.Orders;

namespace Profio.Website.Services;

public interface ICustomerService
{
  Task<ResultModel<PagedListDto<OrderDto>>?> GetCurrentOrdersByPhoneAsync(string phone);
  Task<ResultModel<PagedListDto<OrderDto>>?> GetOrdersByPhoneAsync(string phone);
}
