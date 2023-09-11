using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.Repository.Collections;
using EntityFrameworkCore.Repository.Extensions;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Orders.Validators;
using Profio.Application.Vehicles;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using Profio.Infrastructure.Exceptions;
using Profio.Infrastructure.Persistence;

namespace Profio.Application.Orders.Queries;

public record GetAvailableVehicleByOrderIdWithPagingQuery(string OrderId, Criteria Criteria) : GetWithPagingQueryBase<VehicleDto>(Criteria);
public class GetAvailableVehicleByOrderIdWithPagingQueryHandler : IRequestHandler<GetAvailableVehicleByOrderIdWithPagingQuery, IPagedList<VehicleDto>>
{
  private readonly IRepository<Vehicle> _vehicleRepository;
  private readonly ApplicationDbContext _applicationDbContext;
  private readonly IMapper _mapper;

  public GetAvailableVehicleByOrderIdWithPagingQueryHandler(IRepositoryFactory unitOfWork, ApplicationDbContext applicationDbContext, IMapper mapper)
  {
    _vehicleRepository = unitOfWork.Repository<Vehicle>();
    _applicationDbContext = applicationDbContext;
    _mapper = mapper;
  }
  public async Task<IPagedList<VehicleDto>> Handle(GetAvailableVehicleByOrderIdWithPagingQuery request, CancellationToken cancellationToken)
  {
    var order = await _applicationDbContext.Orders
      .FindAsync(new object?[] { request.OrderId }, cancellationToken: cancellationToken)
      ?? throw new NotFoundException(typeof(Order).Name, request.OrderId);

    var destinationZipCode = order.DestinationZipCode;

    var query = (IMultipleResultQuery<Vehicle>)_vehicleRepository
      .MultipleResultQuery()
      .Page(request.Criteria.PageIndex, request.Criteria.PageSize)
      .AndFilter(x => x.ZipCodeCurrent == destinationZipCode)
      .OrderByDescending(x => x.Id);

    if (request.Criteria.OrderBy is { })
      query = (IMultipleResultQuery<Vehicle>)query
        .OrderBy(request.Criteria.OrderBy);

    if (request.Criteria.OrderByDescending is { })
      query = (IMultipleResultQuery<Vehicle>)query
        .OrderByDescending(request.Criteria.OrderByDescending);

    var pagedListQueryable = _vehicleRepository
      .ToQueryable(query);

    var projectedPagedList = pagedListQueryable
      .ProjectTo<VehicleDto>(_mapper.ConfigurationProvider)
      .AsSplitQuery();

    var asyncPagedList = projectedPagedList.ToListAsync(cancellationToken);

    var pagedList = await asyncPagedList
      .Then<List<VehicleDto>, IList<VehicleDto>>(result => result, cancellationToken)
      .ToPagedListAsync(query.Paging.PageIndex,
        query.Paging.PageSize,
        query.Paging.TotalCount,
        cancellationToken);

    return pagedList;
  }
}
public class GetAvailableVehicleByOrderIdWithPagingQueryValidator : AbstractValidator<GetAvailableVehicleByOrderIdWithPagingQuery>
{
  public GetAvailableVehicleByOrderIdWithPagingQueryValidator(OrderExistenceByIdValidator orderValidator)
  {
    RuleFor(x => x.OrderId)
      .SetValidator(orderValidator);
  }
}
