using Profio.Application.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Routes.Queries;

public class GetRouteWithPagingQueryValidator : GetWithPagingQueryValidatorBase<Route, GetRouteWithPagingQuery, RouteDto>
{
}
