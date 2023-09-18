using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Incidents.Queries;

public sealed record GetIncidentByIdQuery(object Id) : GetByIdQueryBase<IncidentDto>(Id);

public sealed class GetIncidentByIdQueryHandler : GetByIdQueryHandlerBase<GetIncidentByIdQuery, IncidentDto, Incident>
{
  public GetIncidentByIdQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class GetIncidentByIdQueryValidator : GetByIdQueryValidatorBase<GetIncidentByIdQuery, IncidentDto>
{
}
