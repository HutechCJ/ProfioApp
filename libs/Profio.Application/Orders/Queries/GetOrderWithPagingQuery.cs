using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using System.Linq.Expressions;

namespace Profio.Application.Orders.Queries;

public record GetOrderWithPagingQuery(Criteria Criteria, OrderEnumFilter OrderEnumFilter) : GetWithPagingQueryBase<OrderDto>(Criteria);

public class GetOrderWithPagingQueryHandler : GetWithPagingQueryHandler<GetOrderWithPagingQuery, OrderDto, Order>
{
  public GetOrderWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
  protected override Expression<Func<Order, bool>> Filter(string filter)
    => c
      => (c.DestinationZipCode != null && c.DestinationZipCode.ToLower().Contains(filter))
      || (c.Note != null && c.Note.ToLower().Contains(filter))
      || (c.DestinationAddress != null
    && ((c.DestinationAddress.Street != null && c.DestinationAddress.Street.ToLower().Contains(filter))
      || (c.DestinationAddress.Province != null && c.DestinationAddress.Province.ToLower().Contains(filter))
      || (c.DestinationAddress.Ward != null && c.DestinationAddress.Ward.ToLower().Contains(filter))
      || (c.DestinationAddress.City != null && c.DestinationAddress.City.ToLower().Contains(filter)
      || (c.DestinationAddress.ZipCode != null && c.DestinationAddress.ZipCode.ToLower().Contains(filter)))));
  protected override Expression<Func<Order, bool>> RequestFilter(GetOrderWithPagingQuery request)
  {
    return x => request.OrderEnumFilter.Status == null || x.Status == request.OrderEnumFilter.Status;
  }
}

public class
  GetOrderWithPagingQueryValidator : GetWithPagingQueryValidatorBase<GetOrderWithPagingQuery, OrderDto>
{
}
