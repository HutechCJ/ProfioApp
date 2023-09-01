using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Events.Queries;
using Profio.Application.CQRS.Handlers.Queries;
using Profio.Domain.Entities;

namespace Profio.Application.Routes.Queries;

public record GetRouteByIdQuery(object Id) : GetByIdQueryBase<RouteDTO>(Id);
public class GetRouteByIdQueryHandler : GetByIdQueryHandlerBase<GetRouteByIdQuery, RouteDTO, Route>
{
  public GetRouteByIdQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}
