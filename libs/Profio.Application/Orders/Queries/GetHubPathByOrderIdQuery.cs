using AutoMapper;
using EntityFrameworkCore.Repository.Collections;
using EntityFrameworkCore.Repository.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Profio.Application.Hubs;
using Profio.Application.Orders.Validators;
using Profio.Domain.Entities;
using Profio.Domain.Exceptions;
using Profio.Domain.Specifications;
using Profio.Domain.ValueObjects;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Validators;
using Profio.Infrastructure.Persistence;

namespace Profio.Application.Orders.Queries;

public sealed record GetHubPathByOrderIdQuery(string OrderId, Criteria Criteria) : GetWithPagingQueryBase<HubDto>(Criteria);
public sealed class GetHubPathByOrderIdQueryHandler : IRequestHandler<GetHubPathByOrderIdQuery, IPagedList<HubDto>>
{
  private readonly ApplicationDbContext _applicationDbContext;
  private readonly IMapper _mapper;

  public GetHubPathByOrderIdQueryHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
  {

    _applicationDbContext = applicationDbContext;
    _mapper = mapper;
  }
  public async Task<IPagedList<HubDto>> Handle(GetHubPathByOrderIdQuery request, CancellationToken cancellationToken)
  {
    var order = await _applicationDbContext.Orders
      .Include(x => x.Customer)
      .FirstOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken) ?? throw new NotFoundException(typeof(Order).Name, request.OrderId);

    var customerZipCode = order.Customer?.Address?.ZipCode ?? throw new NotFoundException(typeof(Address).Name);

    var destinationZipCode = order.DestinationZipCode;

    var startHub = await _applicationDbContext.Hubs
      .FirstOrDefaultAsync(x => x.ZipCode == customerZipCode, cancellationToken) ?? throw new NotFoundException(typeof(Hub).Name, customerZipCode);

    var endHub = await _applicationDbContext.Hubs
      .FirstOrDefaultAsync(x => x.ZipCode == destinationZipCode, cancellationToken) ?? throw new NotFoundException(typeof(Hub).Name, destinationZipCode);

    IList<HubDto> path = new List<HubDto>
    {
      _mapper.Map<HubDto>(startHub),
      _mapper.Map<HubDto>(endHub)
    };

    var pageIndex = 1;
    var pageSize = path.Count;
    var totalCount = path.Count;

    return path
      .ToPagedList(pageIndex, pageSize, totalCount);
  }
}
public sealed class GetHubPathByOrderIdQueryValidator : GetWithPagingQueryValidatorBase<GetHubPathByOrderIdQuery, HubDto>
{
  public GetHubPathByOrderIdQueryValidator(OrderExistenceByIdValidator orderValidator)
    => RuleFor(x => x.OrderId)
      .SetValidator(orderValidator);
}
