using AutoMapper;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.Repository.Collections;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentEmail.Core.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Profio.Application.Abstractions.CQRS;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Orders.Validators;
using Profio.Application.Vehicles;
using Profio.Domain.Constants;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using Profio.Infrastructure.Exceptions;
using Profio.Infrastructure.Persistence;

namespace Profio.Application.Orders.Queries;

public sealed record GetAvailableVehicleByOrderIdWithPagingQuery(string OrderId, Criteria Criteria) : GetWithPagingQueryBase<VehicleDto>(Criteria);
public sealed class GetAvailableVehicleByOrderIdWithPagingQueryHandler : IRequestHandler<GetAvailableVehicleByOrderIdWithPagingQuery, IPagedList<VehicleDto>>
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
      .Include(x => x.Customer)
      .SingleOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken: cancellationToken)
      ?? throw new NotFoundException(typeof(Order).Name, request.OrderId);

    var customerZipCode = order.Customer?.Address?.ZipCode ?? throw new NotFoundException(typeof(Address).Name);

    var query = (IMultipleResultQuery<Vehicle>)_vehicleRepository
      .MultipleResultQuery()
      .ApplyCriteria(request.Criteria)
      .AndFilter(x => x.ZipCodeCurrent == customerZipCode)
      .AndFilter(x => x.Status == VehicleStatus.Idle);

    var pagedList = await _vehicleRepository
      .GetDataWithQueryAsync<Vehicle, VehicleDto>(query, _mapper.ConfigurationProvider, cancellationToken);

    return pagedList;
  }
}
public sealed class GetAvailableVehicleByOrderIdWithPagingQueryValidator : AbstractValidator<GetAvailableVehicleByOrderIdWithPagingQuery>
{
  public GetAvailableVehicleByOrderIdWithPagingQueryValidator(OrderExistenceByIdValidator orderValidator)
  {
    RuleFor(x => x.OrderId)
      .SetValidator(orderValidator);
  }
}
