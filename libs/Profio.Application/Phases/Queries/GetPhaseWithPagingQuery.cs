using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

namespace Profio.Application.Phases.Queries;

public sealed record GetPhaseWithPagingQuery(Criteria Criteria) : GetWithPagingQueryBase<PhaseDto>(Criteria);

public sealed class GetPhaseWithPagingQueryHandler : GetWithPagingQueryHandler<GetPhaseWithPagingQuery, PhaseDto, Phase>
{
  public GetPhaseWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class
  GetPhaseWithPagingQueryValidator : GetWithPagingQueryValidatorBase<GetPhaseWithPagingQuery, PhaseDto>
{
}
