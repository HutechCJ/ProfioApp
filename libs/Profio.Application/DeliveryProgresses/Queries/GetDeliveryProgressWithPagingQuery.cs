using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

namespace Profio.Application.DeliveryProgresses.Queries;

public sealed record GetDeliveryProgressWithPagingQuery
  (Criteria Criteria) : GetWithPagingQueryBase<DeliveryProgressDto>(Criteria);

public class
  GetDeliveryProgressWithPagingQueryHandler : GetWithPagingQueryHandler<GetDeliveryProgressWithPagingQuery, DeliveryProgressDto, DeliveryProgress>
{

  public GetDeliveryProgressWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
}

public sealed class
  GetDeliveryProgressWithPagingQueryValidator : GetWithPagingQueryValidatorBase<GetDeliveryProgressWithPagingQuery,
    DeliveryProgressDto>
{
}
