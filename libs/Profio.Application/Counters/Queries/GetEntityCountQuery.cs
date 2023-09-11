using MediatR;
using Microsoft.EntityFrameworkCore;
using Profio.Domain.Entities;
using Profio.Infrastructure.Identity;
using Profio.Infrastructure.Persistence;

namespace Profio.Application.Counters.Queries
{
  public record GetEntityCountQuery(IList<string> EntityTypes) : IRequest<Dictionary<string, int>>;

  public class GetEntityCountQueryHandler : IRequestHandler<GetEntityCountQuery, Dictionary<string, int>>
  {
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly Dictionary<string, IQueryable<object>> _entitySets;

    public GetEntityCountQueryHandler(ApplicationDbContext applicationDbContext)
    {
      _applicationDbContext = applicationDbContext;

      _entitySets = new Dictionary<string, IQueryable<object>>
      {
        [nameof(Customer)] = _applicationDbContext.Set<Customer>(),
        [nameof(Delivery)] = _applicationDbContext.Set<Delivery>(),
        [nameof(DeliveryProgress)] = _applicationDbContext.Set<DeliveryProgress>(),
        [nameof(Incident)] = _applicationDbContext.Set<Incident>(),
        [nameof(Order)] = _applicationDbContext.Set<Order>(),
        [nameof(OrderHistory)] = _applicationDbContext.Set<OrderHistory>(),
        [nameof(Route)] = _applicationDbContext.Set<Route>(),
        [nameof(Staff)] = _applicationDbContext.Set<Staff>(),
        [nameof(Vehicle)] = _applicationDbContext.Set<Vehicle>(),
        ["User"] = _applicationDbContext.Set<ApplicationUser>(),
      };
    }

    public async Task<Dictionary<string, int>> Handle(GetEntityCountQuery request, CancellationToken cancellationToken)
    {
      var selectedEntityTypes = _entitySets
          .Where(pair => request.EntityTypes.Contains(pair.Key, StringComparer.OrdinalIgnoreCase))
          .ToDictionary(pair => pair.Key, pair => pair.Value);

      var result = new Dictionary<string, int>();

      foreach (var pair in selectedEntityTypes)
        result[ToCamelCase(pair.Key)] = await pair.Value.CountAsync(cancellationToken: cancellationToken);

      return result;
    }
    private static string ToCamelCase(string input)
    {
      if (string.IsNullOrEmpty(input))
      {
        return input;
      }
      return char.ToLower(input[0]) + input[1..];
    }
  }
}
