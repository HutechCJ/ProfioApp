using MediatR;
using Microsoft.EntityFrameworkCore;
using Profio.Domain.Entities;
using Profio.Domain.Identity;
using Profio.Infrastructure.Persistence;

namespace Profio.Application.Counters.Queries;

public sealed record GetEntityCountQuery(IList<string> EntityTypes) : IRequest<Dictionary<string, int>>;

public sealed class GetEntityCountQueryHandler : IRequestHandler<GetEntityCountQuery, Dictionary<string, int>>
{
  public ApplicationDbContext Context { get; }
  private readonly Dictionary<string, IQueryable<object>> _entitySets;

  public GetEntityCountQueryHandler(ApplicationDbContext applicationDbContext)
  {
    Context = applicationDbContext;

    _entitySets = new()
    {
      [nameof(Hub)] = Context.Set<Hub>(),
      [nameof(Customer)] = Context.Set<Customer>(),
      [nameof(Delivery)] = Context.Set<Delivery>(),
      [nameof(DeliveryProgress)] = Context.Set<DeliveryProgress>(),
      [nameof(Incident)] = Context.Set<Incident>(),
      [nameof(Order)] = Context.Set<Order>(),
      [nameof(OrderHistory)] = Context.Set<OrderHistory>(),
      [nameof(Route)] = Context.Set<Route>(),
      [nameof(Staff)] = Context.Set<Staff>(),
      [nameof(Vehicle)] = Context.Set<Vehicle>(),
      ["User"] = Context.Set<ApplicationUser>(),
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
