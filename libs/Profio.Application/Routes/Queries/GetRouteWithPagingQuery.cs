using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;

namespace Profio.Application.Routes.Queries;

public sealed record GetRouteWithPagingQuery(Criteria Criteria) : GetWithPagingQueryBase<RouteDto>(Criteria);

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
