using Profio.Website.Data.Common;
using Profio.Website.Data.Orders;
using Profio.Website.Repositories;

namespace Profio.Website.Services;

public sealed class CustomerService : ICustomerService
{
  private readonly IRepository _repository;

  public CustomerService(IRepository repository)
    => _repository = repository;

  public Task<ResultModel<PagedListDto<OrderDto>>?> GetCurrentOrdersByPhoneAsync(string phone)
    => _repository.GetAsync<ResultModel<PagedListDto<OrderDto>>?>($"orders/lookup?phone={phone}&current=true");

  public Task<ResultModel<PagedListDto<OrderDto>>?> GetOrdersByPhoneAsync(string phone)
    => _repository.GetAsync<ResultModel<PagedListDto<OrderDto>>?>($"orders/lookup?phone={phone}");
}
