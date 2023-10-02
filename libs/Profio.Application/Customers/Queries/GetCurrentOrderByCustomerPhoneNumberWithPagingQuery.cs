using System.Linq.Expressions;
using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentValidation;
using Profio.Application.Orders.Queries;
using Profio.Domain.Constants;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;

namespace Profio.Application.Customers.Queries;

public sealed record GetCurrentOrderByCustomerPhoneNumberWithPagingQuery
  (string Phone, Specification Specification) : GetOrderWithPagingQuery(Specification, new(null));

public sealed class
  GetCurrentOrderByCustomerPhoneNumberWithPagingQueryHandler : GetOrderWithPagingQueryHandler<
    GetCurrentOrderByCustomerPhoneNumberWithPagingQuery>
{
  public GetCurrentOrderByCustomerPhoneNumberWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) :
    base(unitOfWork, mapper)
  {
  }

  protected override Expression<Func<Order, bool>> RequestFilter(
    GetCurrentOrderByCustomerPhoneNumberWithPagingQuery request)
    => x => new[] { OrderStatus.Pending, OrderStatus.InProgress, OrderStatus.Received }.Contains(x.Status)
            && x.Customer != null && x.Customer.Phone == request.Phone;
}

public sealed class GetCurrentOrderByCustomerPhoneNumberWithPagingQueryValidator : GetOrderWithPagingQueryValidator<
  GetCurrentOrderByCustomerPhoneNumberWithPagingQuery>
{
  public GetCurrentOrderByCustomerPhoneNumberWithPagingQueryValidator()
    => RuleFor(c => c.Phone)
      .Length(10)
      .Matches("^[0-9]*$");
}
