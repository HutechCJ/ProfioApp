using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using System.Linq.Expressions;

namespace Profio.Application.Incidents.Queries;

public record GetIncidentWithPagingQuery(Criteria Criteria, IncidentEnumFilter IncidentEnumFilter) : GetWithPagingQueryBase<IncidentDto>(Criteria);

public class GetIncidentWithPagingQueryHandler : GetWithPagingQueryHandler<GetIncidentWithPagingQuery, IncidentDto, Incident>
{
  public GetIncidentWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
  protected override Expression<Func<Incident, bool>> Filter(string filter)
    => i => i.Description != null && i.Description.ToLower().Contains(filter);
  protected override Expression<Func<Incident, bool>> RequestFilter(GetIncidentWithPagingQuery request)
  {
    return x => request.IncidentEnumFilter.Status == null || x.Status == request.IncidentEnumFilter.Status;
  }
}

public class
  GetIncidentWithPagingQueryValidator : GetWithPagingQueryValidatorBase<GetIncidentWithPagingQuery, IncidentDto>
{
}
