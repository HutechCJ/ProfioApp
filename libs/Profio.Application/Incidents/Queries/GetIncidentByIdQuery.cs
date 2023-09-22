using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

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
