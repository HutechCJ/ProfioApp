using Profio.Application.CQRS;
using Profio.Application.Hubs.Commands;
using Profio.Domain.Entities;

namespace Profio.Application.Hubs;

public class HubProfile : EntityProfileBase<Hub, HubDto, CreateHubCommand, UpdateHubCommand> { }
