using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentValidation;
using Profio.Application.Orders;
using Profio.Application.Orders.Queries;
using Profio.Domain.Constants;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using System.Linq.Expressions;

namespace Profio.Application.Customers.Queries;

public sealed record GetOrderByCustomerPhoneNumberWithPagingQuery(string Phone, Criteria Criteria, OrderEnumFilter OrderEnumFilter, bool Current) : GetOrderWithPagingQuery(Criteria, OrderEnumFilter);

public sealed class GetOrderByCustomerPhoneNumberWithPagingQueryHandler : GetOrderWithPagingQueryHandler<GetOrderByCustomerPhoneNumberWithPagingQuery>
{
  public GetOrderByCustomerPhoneNumberWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }

  protected override Expression<Func<Order, bool>> RequestFilter(GetOrderByCustomerPhoneNumberWithPagingQuery request)
    => x => (!request.Current || new OrderStatus[] { OrderStatus.Pending, OrderStatus.InProgress, OrderStatus.Received }.Contains(x.Status)) && (request.OrderEnumFilter.Status == null || x.Status == request.OrderEnumFilter.Status)
        && (x.Customer != null && x.Customer.Phone == request.Phone);

}
public sealed class GetOrderByCustomerPhoneNumberWithPagingQueryValidator : GetOrderWithPagingQueryValidator<GetOrderByCustomerPhoneNumberWithPagingQuery>
{
  public GetOrderByCustomerPhoneNumberWithPagingQueryValidator()
    => RuleFor(c => c.Phone)
      .Length(10)
      .Matches("^[0-9]*$");
}
