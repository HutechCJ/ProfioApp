using MediatR;
using Microsoft.EntityFrameworkCore;
using Profio.Domain.Entities;
using Profio.Infrastructure.Persistence;
using System.Collections.Immutable;

namespace Profio.Application.Counters.Queries;

public record GetEntityCountQuery(IList<string> EntityTypes) : IRequest<Dictionary<string, int>>;
public class GetEntityCountQueryHandler : IRequestHandler<GetEntityCountQuery, Dictionary<string, int>>
{
  private readonly ApplicationDbContext _applicationDbContext;

  public GetEntityCountQueryHandler(ApplicationDbContext applicationDbContext)
    => _applicationDbContext = applicationDbContext;
  public Task<Dictionary<string, int>> Handle(GetEntityCountQuery request, CancellationToken cancellationToken)
  {
    var entityTypes = new List<Type>
    {
      typeof(Customer),
      typeof(Delivery),
      typeof(Incident),
      typeof(Order),
      typeof(Staff),
      typeof(Vehicle)
    };

    var selectedEntityTypes = entityTypes.Where(et => request.EntityTypes.Select(x => x.ToLower()).Contains(et.Name.ToLower()));

    var dictionary = selectedEntityTypes.ToDictionary(x => x.Name, x => GetCount(x));

    return Task.FromResult(dictionary);
  }

  private int GetCount(Type type)
  {
    return type.Name switch
    {
      nameof(Customer) => _applicationDbContext.Set<Customer>().Count(),
      nameof(Delivery) => _applicationDbContext.Set<Delivery>().Count(),
      nameof(Incident) => _applicationDbContext.Set<Incident>().Count(),
      nameof(Order) => _applicationDbContext.Set<Order>().Count(),
      nameof(Staff) => _applicationDbContext.Set<Staff>().Count(),
      nameof(Vehicle) => _applicationDbContext.Set<Vehicle>().Count(),
      _ => 0
    };
  }
}
