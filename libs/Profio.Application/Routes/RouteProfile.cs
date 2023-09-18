using Profio.Application.Abstractions.CQRS;
using Profio.Application.Routes.Commands;
using Profio.Domain.Entities;

namespace Profio.Application.Routes;

public sealed class RouteProfile : EntityProfileBase<Route, RouteDto, CreateRouteCommand, UpdateRouteCommand> { }
