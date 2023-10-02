using System.Linq.Expressions;
using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

namespace Profio.Application.Vehicles.Queries;

public sealed record GetVehicleWithPagingQuery
  (Specification Specification, VehicleEnumFilter VehicleEnumFilter) : GetWithPagingQueryBase<VehicleDto>(Specification);

public sealed class
  GetVehicleWithPagingQueryHandler : GetWithPagingQueryHandler<GetVehicleWithPagingQuery, VehicleDto, Vehicle>
{
  public GetVehicleWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }

  protected override Expression<Func<Vehicle, bool>> Filter(string filter) =>
    v =>
      (v.ZipCodeCurrent != null && v.ZipCodeCurrent.ToLower().Contains(filter))
      || (v.LicensePlate != null && v.LicensePlate.ToLower().Contains(filter));

  protected override Expression<Func<Vehicle, bool>> RequestFilter(GetVehicleWithPagingQuery request) =>
    x => (request.VehicleEnumFilter.Status == null || x.Status == request.VehicleEnumFilter.Status)
         && (request.VehicleEnumFilter.Type == null || x.Type == request.VehicleEnumFilter.Type);
}

public sealed class
  GetVehicleWithPagingQueryValidator : GetWithPagingQueryValidatorBase<GetVehicleWithPagingQuery, VehicleDto>
{
}
