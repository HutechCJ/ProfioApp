using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Domain.Entities;

namespace Profio.Application.Routes.Queries;

public record GetRouteByIdQuery(object Id) : GetByIdQueryBase<RouteDto>(Id);

public class GetRouteByIdQueryHandler : GetByIdQueryHandlerBase<GetRouteByIdQuery, RouteDto, Route>
{
  public GetRouteByIdQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}
