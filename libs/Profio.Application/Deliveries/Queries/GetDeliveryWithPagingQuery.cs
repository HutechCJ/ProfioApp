using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;

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
