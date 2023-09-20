using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

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
