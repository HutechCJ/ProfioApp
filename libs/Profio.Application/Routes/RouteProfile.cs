using Profio.Application.Routes.Commands;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS;

namespace Profio.Application.Routes;

public sealed class RouteProfile : EntityProfileBase<Route, RouteDto, CreateRouteCommand, UpdateRouteCommand> { }
