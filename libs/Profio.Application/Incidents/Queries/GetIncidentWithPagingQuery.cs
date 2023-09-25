using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Validators;
using System.Linq.Expressions;

namespace Profio.Application.Incidents.Queries;

public sealed record GetIncidentWithPagingQuery(Criteria Criteria, IncidentEnumFilter IncidentEnumFilter) : GetWithPagingQueryBase<IncidentDto>(Criteria);

public sealed class GetIncidentWithPagingQueryHandler : GetWithPagingQueryHandler<GetIncidentWithPagingQuery, IncidentDto, Incident>
{
  public GetIncidentWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
  protected override Expression<Func<Incident, bool>> Filter(string filter)
    => i => i.Description != null && i.Description.ToLower().Contains(filter);
  protected override Expression<Func<Incident, bool>> RequestFilter(GetIncidentWithPagingQuery request)
    => x => request.IncidentEnumFilter.Status == null || x.Status == request.IncidentEnumFilter.Status;
  
}

public sealed class
  GetIncidentWithPagingQueryValidator : GetWithPagingQueryValidatorBase<GetIncidentWithPagingQuery, IncidentDto>
{
}
