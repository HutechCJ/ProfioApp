using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Events.Queries;
using Profio.Application.CQRS.Handlers.Queries;
using Profio.Application.CQRS.Validators;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using System.Linq.Expressions;

namespace Profio.Application.Incidents.Queries;

public record GetIncidentWithPagingQuery(Criteria<Incident> Criteria) : GetWithPagingQueryBase<Incident, IncidentDto>(Criteria);

public class GetIncidentWithPagingQueryHandler : GetWithPagingQueryHandler<GetIncidentWithPagingQuery, IncidentDto, Incident>
{
  public GetIncidentWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
  protected override Expression<Func<Incident, bool>> Filter(string filter)
    => i => i.Description != null && i.Description.ToLower().Contains(filter);
}

public class
  GetIncidentWithPagingQueryValidator : GetWithPagingQueryValidatorBase<Incident, GetIncidentWithPagingQuery, IncidentDto>
{
}
