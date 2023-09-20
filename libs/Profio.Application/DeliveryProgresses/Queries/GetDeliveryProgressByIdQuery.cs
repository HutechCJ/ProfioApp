using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

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
