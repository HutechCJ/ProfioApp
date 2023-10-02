using Profio.Application.Hubs.Commands;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS;

namespace Profio.Application.Hubs;

public sealed class HubProfile : EntityProfileBase<Hub, HubDto, CreateHubCommand, UpdateHubCommand>
{
}
