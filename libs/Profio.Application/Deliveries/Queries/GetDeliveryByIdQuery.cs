using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

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
