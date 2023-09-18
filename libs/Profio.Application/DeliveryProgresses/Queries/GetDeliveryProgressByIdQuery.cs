using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.DeliveryProgresses.Queries;

public sealed record GetDeliveryProgressByIdQuery(object Id) : GetByIdQueryBase<DeliveryProgressDto>(Id);

public sealed class GetDeliveryProgressByIdQueryHandler : GetByIdQueryHandlerBase<GetDeliveryProgressByIdQuery, DeliveryProgressDto, DeliveryProgress>
{
  public GetDeliveryProgressByIdQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class GetDeliveryProgressByIdQueryValidator : GetByIdQueryValidatorBase<GetDeliveryProgressByIdQuery, DeliveryProgressDto>
{
}
