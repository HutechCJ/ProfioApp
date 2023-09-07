using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Events.Queries;
using Profio.Application.CQRS.Handlers.Queries;
using Profio.Application.CQRS.Validators;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;

namespace Profio.Application.Deliveries.Queries;

public record GetDeliveryWithPagingQuery
  (Criteria<Delivery> Criteria) : GetWithPagingQueryBase<Delivery, DeliveryDto>(Criteria);

public class
  GetDeliveryWithPagingQueryHandler : GetWithPagingQueryHandler<GetDeliveryWithPagingQuery, DeliveryDto, Delivery>
{

  public GetDeliveryWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
}

public class
  GetDeliveryWithPagingQueryValidator : GetWithPagingQueryValidatorBase<Delivery, GetDeliveryWithPagingQuery,
    DeliveryDto>
{
}
