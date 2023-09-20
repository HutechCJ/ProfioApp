using Profio.Application.Phases.Commands;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS;

namespace Profio.Application.Phases;

public class PhaseProfile : EntityProfileBase<Phase, PhaseDto, CreatePhaseCommand, UpdatePhaseCommand>
{
}
