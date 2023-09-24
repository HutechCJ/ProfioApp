using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Orders;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Validators;
using System.Linq.Expressions;

namespace Profio.Application.Vehicles.Queries;

public sealed record GetOrderByVehicleIdWithPagingQuery(string VehicleId, Criteria Criteria) : GetWithPagingQueryBase<OrderDto>(Criteria);
sealed class GetOrderByVehicleIdWithPagingQueryHandler : GetWithPagingQueryHandler<GetOrderByVehicleIdWithPagingQuery, OrderDto, Order>
{
  public GetOrderByVehicleIdWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
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
  protected override Expression<Func<Order, bool>> RequestFilter(GetOrderByVehicleIdWithPagingQuery request)
  {
    return x => x.Deliveries != null && x.Deliveries.Any(d => d.VehicleId == request.VehicleId);
  }
}
public class GetOrderByVehicleIdWithPagingQueryValidator : GetWithPagingQueryValidatorBase<GetOrderByVehicleIdWithPagingQuery, OrderDto>
{
  public GetOrderByVehicleIdWithPagingQueryValidator()
  {
  }
}
