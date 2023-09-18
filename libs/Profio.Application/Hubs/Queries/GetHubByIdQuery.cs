using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Hubs.Queries;

public sealed record GetHubByIdQuery(object Id) : GetByIdQueryBase<HubDto>(Id);

public sealed class GetHubByIdQueryHandler : GetByIdQueryHandlerBase<GetHubByIdQuery, HubDto, Hub>
{
  public GetHubByIdQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class GetHubByIdQueryValidator : GetByIdQueryValidatorBase<GetHubByIdQuery, HubDto>
{
}
