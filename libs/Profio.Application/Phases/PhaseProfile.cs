using Profio.Application.Abstractions.CQRS;
using Profio.Application.Phases.Commands;
using Profio.Domain.Entities;

namespace Profio.Application.Phases;

public class PhaseProfile : EntityProfileBase<Phase, PhaseDto, CreatePhaseCommand, UpdatePhaseCommand>
{
}
