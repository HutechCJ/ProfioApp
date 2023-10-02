using Profio.Website.Data.Common;
using Profio.Website.Data.Orders;

namespace Profio.Website.Services;

public interface ICustomerService
{
  public Task<ResultModel<PagedListDto<OrderDto>>?> GetCurrentOrdersByPhoneAsync(string phone);
  public Task<ResultModel<PagedListDto<OrderDto>>?> GetOrdersByPhoneAsync(string phone);
}
