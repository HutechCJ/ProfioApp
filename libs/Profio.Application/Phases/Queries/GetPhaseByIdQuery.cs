using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Domain.Entities;

namespace Profio.Application.Phases.Queries;

public sealed record GetPhaseByIdQuery(object Id) : GetByIdQueryBase<PhaseDto>(Id);

sealed class GetPhaseByIdQueryHandler : GetByIdQueryHandlerBase<GetPhaseByIdQuery, PhaseDto, Phase>
{
  public GetPhaseByIdQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}
