using Profio.Website.Data.Common;
using Profio.Website.Data.Orders;
using Profio.Website.Repositories;

namespace Profio.Website.Services;

public sealed class CustomerService : Repository, ICustomerService
{
  public CustomerService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
  {
  }

  public Task<ResultModel<PagedListDto<OrderDto>>?> GetCurrentOrdersByPhoneAsync(string phone)
    => GetAsync<ResultModel<PagedListDto<OrderDto>>?>($"customers/{phone}/orders/current");

  public Task<ResultModel<PagedListDto<OrderDto>>?> GetOrdersByPhoneAsync(string phone)
    => GetAsync<ResultModel<PagedListDto<OrderDto>>?>($"customers/{phone}/orders");
}
