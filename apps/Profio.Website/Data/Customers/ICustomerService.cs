using Profio.Website.Data.Common.Dtos;
using Profio.Website.Data.Orders;

namespace Profio.Website.Data.Customers;

public interface ICustomerService
{
  Task<ResultModel<PagedListDto<OrderDto>>?> GetCurrentOrdersByPhoneAsync(string phone);
  Task<ResultModel<PagedListDto<OrderDto>>?> GetOrdersByPhoneAsync(string phone);
}
