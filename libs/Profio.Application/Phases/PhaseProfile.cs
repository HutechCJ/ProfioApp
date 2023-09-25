using Profio.Application.Phases.Commands;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS;

namespace Profio.Application.Phases;

public sealed class PhaseProfile : EntityProfileBase<Phase, PhaseDto, CreatePhaseCommand, UpdatePhaseCommand>
{
}
