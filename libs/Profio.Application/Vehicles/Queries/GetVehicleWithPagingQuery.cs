using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Validators;
using System.Linq.Expressions;

namespace Profio.Application.Vehicles.Queries;

public sealed record GetVehicleWithPagingQuery
  (Criteria Criteria, VehicleEnumFilter VehicleEnumFilter) : GetWithPagingQueryBase<VehicleDto>(Criteria);

public sealed class
  GetVehicleWithPagingQueryHandler : GetWithPagingQueryHandler<GetVehicleWithPagingQuery, VehicleDto, Vehicle>
{
  public GetVehicleWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
  protected override Expression<Func<Vehicle, bool>> Filter(string filter)
  {
    return v =>
                (v.ZipCodeCurrent != null && v.ZipCodeCurrent.ToLower().Contains(filter))
             || (v.LicensePlate != null && v.LicensePlate.ToLower().Contains(filter));
  }
  protected override Expression<Func<Vehicle, bool>> RequestFilter(GetVehicleWithPagingQuery request)
  {
    return x => (request.VehicleEnumFilter.Status == null || x.Status == request.VehicleEnumFilter.Status)
    && (request.VehicleEnumFilter.Type == null || x.Type == request.VehicleEnumFilter.Type);
  }
}

public sealed class
  GetVehicleWithPagingQueryValidator : GetWithPagingQueryValidatorBase<GetVehicleWithPagingQuery, VehicleDto>
{
}
