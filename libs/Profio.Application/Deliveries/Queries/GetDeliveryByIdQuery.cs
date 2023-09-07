using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Events.Queries;
using Profio.Application.CQRS.Handlers.Queries;
using Profio.Application.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Deliveries.Queries;

public record GetDeliveryByIdQuery(object Id) : GetByIdQueryBase<DeliveryDto>(Id);

public class GetDeliveryByIdQueryHandler : GetByIdQueryHandlerBase<GetDeliveryByIdQuery, DeliveryDto, Delivery>
{
  public GetDeliveryByIdQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class GetDeliveryByIdQueryValidator : GetByIdQueryValidatorBase<GetDeliveryByIdQuery, DeliveryDto>
{
}
