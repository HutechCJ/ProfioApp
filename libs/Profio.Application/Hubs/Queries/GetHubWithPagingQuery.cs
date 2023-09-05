using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Events.Queries;
using Profio.Application.CQRS.Handlers.Queries;
using Profio.Application.CQRS.Validators;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;

namespace Profio.Application.Hubs.Queries;

  public record GetHubWithPagingQuery(Criteria<Hub> Criteria) : GetWithPagingQueryBase<Hub, HubDto>(Criteria);

  public class GetHubWithPagingQueryHandler : GetWithPagingQueryHandler<GetHubWithPagingQuery, HubDto, Hub>
  {
    public GetHubWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
  }
  
public class GetHubWithPagingQueryValidator : GetWithPagingQueryValidatorBase<Hub, GetHubWithPagingQuery, HubDto> { }