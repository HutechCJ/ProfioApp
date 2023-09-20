using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

namespace Profio.Application.Deliveries.Queries;

public sealed record GetDeliveryWithPagingQuery
  (Criteria Criteria) : GetWithPagingQueryBase<DeliveryDto>(Criteria);

public sealed class
  GetDeliveryWithPagingQueryHandler : GetWithPagingQueryHandler<GetDeliveryWithPagingQuery, DeliveryDto, Delivery>
{

  public GetDeliveryWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
}

public sealed class
  GetDeliveryWithPagingQueryValidator : GetWithPagingQueryValidatorBase<GetDeliveryWithPagingQuery,
    DeliveryDto>
{
}
