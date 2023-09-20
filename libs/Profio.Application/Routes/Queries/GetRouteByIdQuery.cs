using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Queries;

namespace Profio.Application.Routes.Queries;

public sealed record GetRouteByIdQuery(object Id) : GetByIdQueryBase<RouteDto>(Id);

public sealed class GetRouteByIdQueryHandler : GetByIdQueryHandlerBase<GetRouteByIdQuery, RouteDto, Route>
{
  public GetRouteByIdQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}
