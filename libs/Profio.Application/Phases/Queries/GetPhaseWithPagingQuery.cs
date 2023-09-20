using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;

namespace Profio.Application.Phases.Queries;

public sealed record GetPhaseWithPagingQuery(Criteria Criteria) : GetWithPagingQueryBase<PhaseDto>(Criteria);

sealed class GetPhaseWithPagingQueryHandler : GetWithPagingQueryHandler<GetPhaseWithPagingQuery, PhaseDto, Phase>
{
  public GetPhaseWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

sealed class
  GetPhaseWithPagingQueryValidator : GetWithPagingQueryValidatorBase<GetPhaseWithPagingQuery, PhaseDto>
{
}
