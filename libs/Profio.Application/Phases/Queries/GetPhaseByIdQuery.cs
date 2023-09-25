using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Queries;

namespace Profio.Application.Phases.Queries;

public sealed record GetPhaseByIdQuery(object Id) : GetByIdQueryBase<PhaseDto>(Id);

public sealed class GetPhaseByIdQueryHandler : GetByIdQueryHandlerBase<GetPhaseByIdQuery, PhaseDto, Phase>
{
  public GetPhaseByIdQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}
