using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Events.Queries;
using Profio.Application.CQRS.Handlers.Queries;
using Profio.Application.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.DeliveryProgresses.Queries;

public record GetDeliveryProgressByIdQuery(object Id) : GetByIdQueryBase<DeliveryProgressDto>(Id);

public class GetDeliveryProgressByIdQueryHandler : GetByIdQueryHandlerBase<GetDeliveryProgressByIdQuery, DeliveryProgressDto, DeliveryProgress>
{
  public GetDeliveryProgressByIdQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class GetDeliveryProgressByIdQueryValidator : GetByIdQueryValidatorBase<GetDeliveryProgressByIdQuery, DeliveryProgressDto>
{
}
