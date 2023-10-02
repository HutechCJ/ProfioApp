using AutoMapper;
using EntityFrameworkCore.Repository.Collections;
using EntityFrameworkCore.Repository.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Profio.Application.Hubs;
using Profio.Application.Vehicles.Validators;
using Profio.Domain.Constants;
using Profio.Domain.Entities;
using Profio.Domain.Exceptions;
using Profio.Domain.Specifications;
using Profio.Domain.ValueObjects;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Validators;
using Profio.Infrastructure.Persistence;

namespace Profio.Application.Vehicles.Queries;

public sealed record GetHubPathByVehicleIdQuery
  (string VehicleId, Specification Specification) : GetWithPagingQueryBase<HubDto>(Specification);

public sealed class GetHubPathByVehicleIdQueryHandler : IRequestHandler<GetHubPathByVehicleIdQuery, IPagedList<HubDto>>
{
  private readonly ApplicationDbContext _applicationDbContext;
  private readonly IMapper _mapper;

  public GetHubPathByVehicleIdQueryHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
    => (_applicationDbContext, _mapper) = (applicationDbContext, mapper);

  public async Task<IPagedList<HubDto>> Handle(GetHubPathByVehicleIdQuery request, CancellationToken cancellationToken)
  {
    var order = await _applicationDbContext.Orders
      .Include(x => x.Customer)
      .OrderByDescending(x => x.StartedDate)
      .Where(x => new[] { OrderStatus.InProgress, OrderStatus.Completed, OrderStatus.Pending }.Contains(x.Status))
      .FirstOrDefaultAsync(x => x.Deliveries != null && x.Deliveries.Any(d => d.VehicleId == request.VehicleId),
        cancellationToken) ?? throw new NotFoundException(nameof(Order), "active newest");

    var customerZipCode = order.Customer?.Address?.ZipCode ?? throw new NotFoundException(nameof(Address));

    var destinationZipCode = order.DestinationZipCode;

    var startHub = await _applicationDbContext.Hubs
                     .FirstOrDefaultAsync(x => x.ZipCode == customerZipCode, cancellationToken) ??
                   throw new NotFoundException(nameof(Hub), customerZipCode);

    var endHub = await _applicationDbContext.Hubs
                   .FirstOrDefaultAsync(x => x.ZipCode == destinationZipCode, cancellationToken) ??
                 throw new NotFoundException(nameof(Hub), destinationZipCode);

    IList<HubDto> path = new List<HubDto>
    {
      _mapper.Map<HubDto>(startHub),
      _mapper.Map<HubDto>(endHub)
    };

    const int pageIndex = 1;
    var pageSize = path.Count;
    var totalCount = path.Count;

    return path
      .ToPagedList(pageIndex, pageSize, totalCount);
  }
}

public sealed class
  GetHubPathByVehicleIdQueryValidator : GetWithPagingQueryValidatorBase<GetHubPathByVehicleIdQuery, HubDto>
{
  public GetHubPathByVehicleIdQueryValidator(VehicleExistenceByIdValidator vehicleValidator)
    => RuleFor(x => x.VehicleId)
      .SetValidator(vehicleValidator);
}
