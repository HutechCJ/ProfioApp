using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

namespace Profio.Application.Routes.Queries;

public sealed record GetRouteWithPagingQuery(Specification Specification) : GetWithPagingQueryBase<RouteDto>(Specification);

public sealed class GetRouteWithPagingQueryHandler : GetWithPagingQueryHandler<GetRouteWithPagingQuery, RouteDto, Route>
{
  public GetRouteWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class
  GetRouteWithPagingQueryValidator : GetWithPagingQueryValidatorBase<GetRouteWithPagingQuery, RouteDto>
{
}
