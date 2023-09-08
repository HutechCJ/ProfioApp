using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;

namespace Profio.Application.DeliveryProgresses.Queries;

public record GetDeliveryProgressWithPagingQuery
  (Criteria<DeliveryProgress> Criteria) : GetWithPagingQueryBase<DeliveryProgress, DeliveryProgressDto>(Criteria);

public class
  GetDeliveryProgressWithPagingQueryHandler : GetWithPagingQueryHandler<GetDeliveryProgressWithPagingQuery, DeliveryProgressDto, DeliveryProgress>
{

  public GetDeliveryProgressWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
}

public class
  GetDeliveryProgressWithPagingQueryValidator : GetWithPagingQueryValidatorBase<DeliveryProgress, GetDeliveryProgressWithPagingQuery,
    DeliveryProgressDto>
{
}
