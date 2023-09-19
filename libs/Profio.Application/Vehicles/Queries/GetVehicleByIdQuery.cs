using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Vehicles.Queries;

public sealed record GetVehicleByIdQuery(object Id) : GetByIdQueryBase<VehicleDto>(Id);

public sealed class GetVehicleByIdQueryHandler : GetByIdQueryHandlerBase<GetVehicleByIdQuery, VehicleDto, Vehicle>
{
  public GetVehicleByIdQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class GetVehicleByIdQueryValidator : GetByIdQueryValidatorBase<GetVehicleByIdQuery, VehicleDto>
{
}
