using Profio.Application.Abstractions.CQRS;
using Profio.Application.Hubs.Commands;
using Profio.Domain.Entities;

namespace Profio.Application.Hubs;

public sealed class HubProfile : EntityProfileBase<Hub, HubDto, CreateHubCommand, UpdateHubCommand> { }
