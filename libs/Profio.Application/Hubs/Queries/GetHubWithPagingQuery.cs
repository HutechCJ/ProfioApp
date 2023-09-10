using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using System.Linq.Expressions;

namespace Profio.Application.Hubs.Queries;

public record GetHubWithPagingQuery(Criteria Criteria) : GetWithPagingQueryBase<HubDto>(Criteria);

public class GetHubWithPagingQueryHandler : GetWithPagingQueryHandler<GetHubWithPagingQuery, HubDto, Hub>
{
  public GetHubWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }

  protected override Expression<Func<Hub, bool>> Filter(string filter)
    => h => h.Name!.ToLower().Contains(filter);
}

public class GetHubWithPagingQueryValidator : GetWithPagingQueryValidatorBase<GetHubWithPagingQuery, HubDto>
{
}
