using Profio.Website.Data.Common;
using Profio.Website.Data.Common.Dtos;
using Profio.Website.Data.Orders;

namespace Profio.Website.Data.Customers;

public sealed class CustomerService : BaseApiService, ICustomerService
{
  public CustomerService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
  {
  }

  public Task<ResultModel<PagedListDto<OrderDto>>?> GetCurrentOrdersByPhoneAsync(string phone)
        => GetAsync<ResultModel<PagedListDto<OrderDto>>?>($"customers/{phone}/orders/current");

  public Task<ResultModel<PagedListDto<OrderDto>>?> GetOrdersByPhoneAsync(string phone)
    => GetAsync<ResultModel<PagedListDto<OrderDto>>?>($"customers/{phone}/orders");
}
