using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using System.Linq.Expressions;

namespace Profio.Application.Hubs.Queries;

public record GetHubWithPagingQuery(Criteria Criteria, HubEnumFilter HubEnumFilter) : GetWithPagingQueryBase<HubDto>(Criteria);

public class GetHubWithPagingQueryHandler : GetWithPagingQueryHandler<GetHubWithPagingQuery, HubDto, Hub>
{
  public GetHubWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }

  protected override Expression<Func<Hub, bool>> Filter(string filter)
    => h
      => (h.Name != null && h.Name.ToLower().Contains(filter))
      || (h.ZipCode != null && h.ZipCode.ToLower().Contains(filter))
      || (h.Address != null && ((h.Address.Street != null
        && h.Address.Street.ToLower().Contains(filter))
      || (h.Address.Province != null
        && h.Address.Province.ToLower().Contains(filter))
      || (h.Address.Ward != null
        && h.Address.Ward.ToLower().Contains(filter))
      || (h.Address.City != null
        && h.Address.City.ToLower().Contains(filter))
      || (h.Address.ZipCode != null
        && h.Address.ZipCode.ToLower().Contains(filter))
    ));
  protected override Expression<Func<Hub, bool>> RequestFilter(GetHubWithPagingQuery request)
  {
    return x => request.HubEnumFilter.Status == null || x.Status == request.HubEnumFilter.Status;
  }
}

public class GetHubWithPagingQueryValidator : GetWithPagingQueryValidatorBase<GetHubWithPagingQuery, HubDto>
{
}
