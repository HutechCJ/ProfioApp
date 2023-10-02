using System.Linq.Expressions;
using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Customers.Validators;
using Profio.Application.Orders;
using Profio.Application.Orders.Queries;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;

namespace Profio.Application.Customers.Queries;

public sealed record GetOrderByCustomerIdWithPagingQuery(string CustomerId, Criteria Criteria,
  OrderEnumFilter OrderEnumFilter) : GetOrderWithPagingQuery(Criteria, OrderEnumFilter);

public sealed class
  GetOrderByCustomerIdWithPagingQueryHandler : GetOrderWithPagingQueryHandler<GetOrderByCustomerIdWithPagingQuery>
{
  public GetOrderByCustomerIdWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork,
    mapper)
  {
  }

  protected override Expression<Func<Order, bool>> RequestFilter(GetOrderByCustomerIdWithPagingQuery request)
    => x => (request.OrderEnumFilter.Status == null || x.Status == request.OrderEnumFilter.Status)
            && x.Customer != null && x.CustomerId == request.CustomerId;
}

public sealed class
  GetOrderByCustomerIdWithPagingQueryValidator : GetOrderWithPagingQueryValidator<GetOrderByCustomerIdWithPagingQuery>
{
  public GetOrderByCustomerIdWithPagingQueryValidator(CustomerExistenceByIdValidator customerValidator)
    => RuleFor(x => x.CustomerId)
      .SetValidator(customerValidator);
}
