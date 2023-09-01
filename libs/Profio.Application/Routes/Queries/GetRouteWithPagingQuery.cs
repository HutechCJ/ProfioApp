using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Events.Queries;
using Profio.Application.CQRS.Handlers.Queries;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;

namespace Profio.Application.Routes.Queries;

public record GetRouteWithPagingQuery(Criteria<Route> Criteria) : GetWithPagingQueryBase<Route, RouteDTO>(Criteria);
public class GetRouteWithPagingQueryHandler : GetWithPagingQueryHandler<GetRouteWithPagingQuery, RouteDTO, Route>
{
  public GetRouteWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}
