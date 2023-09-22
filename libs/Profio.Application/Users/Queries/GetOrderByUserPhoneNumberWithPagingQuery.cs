using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentValidation;
using Profio.Application.Orders;
using Profio.Application.Orders.Queries;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using System.Linq.Expressions;

namespace Profio.Application.Users.Queries;

public sealed record GetOrderByUserPhoneNumberWithPagingQuery(string Phone, Criteria Criteria, OrderEnumFilter OrderEnumFilter) : GetOrderWithPagingQuery(Criteria, OrderEnumFilter);
sealed class GetOrderByUserPhoneNumberWithPagingQueryHandler : GetOrderWithPagingQueryHandler<GetOrderByUserPhoneNumberWithPagingQuery>
{
  public GetOrderByUserPhoneNumberWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
  protected override Expression<Func<Order, bool>> RequestFilter(GetOrderByUserPhoneNumberWithPagingQuery request)
  {
    return x => (request.OrderEnumFilter.Status == null || x.Status == request.OrderEnumFilter.Status)
    && (x.Customer != null && x.Customer.Phone == request.Phone);
  }
}
public sealed class GetOrderByUserPhoneNumberWithPagingQueryValidator : GetOrderWithPagingQueryValidator<GetOrderByUserPhoneNumberWithPagingQuery>
{
  public GetOrderByUserPhoneNumberWithPagingQueryValidator()
    => RuleFor(c => c.Phone)
      .Length(10)
      .Matches("^[0-9]*$");
}
