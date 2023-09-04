using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Events.Queries;
using Profio.Application.CQRS.Handlers.Queries;
using Profio.Application.CQRS.Validators;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;

namespace Profio.Application.Routes.Queries;

public record GetRouteWithPagingQuery(Criteria<Route> Criteria) : GetWithPagingQueryBase<Route, RouteDto>(Criteria);
public class GetRouteWithPagingQueryHandler : GetWithPagingQueryHandler<GetRouteWithPagingQuery, RouteDto, Route>
{
  public GetRouteWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}
public class GetRouteWithPagingQueryValidator : GetWithPagingQueryValidatorBase<Route, GetRouteWithPagingQuery, RouteDto>
{
}
