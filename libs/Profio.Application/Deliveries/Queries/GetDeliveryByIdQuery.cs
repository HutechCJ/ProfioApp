using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Deliveries.Queries;

public sealed record GetDeliveryByIdQuery(object Id) : GetByIdQueryBase<DeliveryDto>(Id);

public sealed class GetDeliveryByIdQueryHandler : GetByIdQueryHandlerBase<GetDeliveryByIdQuery, DeliveryDto, Delivery>
{
  public GetDeliveryByIdQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class GetDeliveryByIdQueryValidator : GetByIdQueryValidatorBase<GetDeliveryByIdQuery, DeliveryDto>
{
}
