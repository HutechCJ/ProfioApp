using System.Linq.Expressions;
using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

namespace Profio.Application.Orders.Queries;

public record GetOrderWithPagingQuery
  (Specification Specification, OrderEnumFilter OrderEnumFilter) : GetWithPagingQueryBase<OrderDto>(Specification);

public class GetOrderWithPagingQueryHandler<TQuery> : GetWithPagingQueryHandler<TQuery, OrderDto, Order>
  where TQuery : GetOrderWithPagingQuery
{
  public GetOrderWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }

  protected override Expression<Func<Order, bool>> Filter(string filter)
    => c
      => c.DestinationZipCode.ToLower().Contains(filter)
         || (c.Note != null && c.Note.ToLower().Contains(filter))
         || (c.DestinationAddress != null
             && ((c.DestinationAddress.Street != null && c.DestinationAddress.Street.ToLower().Contains(filter))
                 || (c.DestinationAddress.Province != null && c.DestinationAddress.Province.ToLower().Contains(filter))
                 || (c.DestinationAddress.Ward != null && c.DestinationAddress.Ward.ToLower().Contains(filter))
                 || (c.DestinationAddress.City != null && c.DestinationAddress.City.ToLower().Contains(filter))
                 || (c.DestinationAddress.ZipCode != null && c.DestinationAddress.ZipCode.ToLower().Contains(filter))));

  protected override Expression<Func<Order, bool>> RequestFilter(TQuery request)
    => x => request.OrderEnumFilter.Status == null || x.Status == request.OrderEnumFilter.Status;
}

public sealed class GetOrderWithPagingQueryHandler : GetOrderWithPagingQueryHandler<GetOrderWithPagingQuery>
{
  public GetOrderWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class
  GetOrderWithPagingQueryValidator<TQuery> : GetWithPagingQueryValidatorBase<TQuery, OrderDto>
  where TQuery : GetOrderWithPagingQuery
{
}

public sealed class GetOrderWithPagingQueryValidator : GetOrderWithPagingQueryValidator<GetOrderWithPagingQuery>
{
}
