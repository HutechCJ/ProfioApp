using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Profio.Domain.Interfaces;
using Profio.Domain.Specifications;

namespace Profio.Infrastructure.Persistence.Relational;

public class RelationalRepository<T> : IRelationalRepository<T>
  where T : class
{
  private readonly ApplicationDbContext _context;
  private readonly DbSet<T> _dbSet;

  public RelationalRepository(ApplicationDbContext context)
    => (_context, _dbSet) = (context, context.Set<T>());

  public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    => await _dbSet.AddAsync(entity, cancellationToken);

  public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
  {
    _context.Entry(entity).State = EntityState.Modified;
    return Task.CompletedTask;
  }

  public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
  {
    _dbSet.Remove(entity);
    await Task.CompletedTask;
  }

  public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default) => throw new NotImplementedException();

  public async Task<IEnumerable<T>> GetByAsync(
    Expression<Func<T, bool>> predicate,
    CancellationToken cancellationToken = default)
    => await _dbSet.Where(predicate).ToListAsync(cancellationToken);

  public IQueryable<T> GetPaging(ISpecification<T> specification)
  {
    var query = _dbSet.AsQueryable();

    if (specification.Filter is { })
      _ = query.Where(specification.Filter);

    _ = specification
      .Includes
      .Aggregate(query, (current, include) => current.Include(include));

    _ = specification
      .IncludeStrings
      .Aggregate(query, (current, include) => current.Include(include));

    if (specification.OrderBy is { })
      _ = query.OrderBy(specification.OrderBy);

    if (specification.OrderByDescending is { })
      _ = query.OrderByDescending(specification.OrderByDescending);

    if (specification.GroupBy is { })
      _ = query.GroupBy(specification.GroupBy);

    if (!string.IsNullOrEmpty(specification.Cursor))
    {
      var cursor = JsonConvert.DeserializeObject<T>(specification.Cursor);
      var conversionSuccessful = int.TryParse(
        cursor?
          .GetType()
          .GetProperties()
          .FirstOrDefault(x => x.Name.Equals("Id"))?
          .GetValue(cursor)?
          .ToString(),
        out var cursorValue);

      if (conversionSuccessful)
        cursorValue = 0;

      _ = specification.IsAscending
        ? query.Where(x => (int)x.GetType().GetProperty("Id")!.GetValue(x)! > cursorValue)
        : query.Where(x => (int)x.GetType().GetProperty("Id")!.GetValue(x)! < cursorValue);
    }

    if (specification.IsPagingEnabled)
      _ = query.Skip(specification.Skip - 1).Take(specification.Take);

    return query.AsSplitQuery().AsNoTracking();
  }

  public IEnumerable<T> GetShaping(IEnumerable<T> entities, string fieldsString)
  {
    ArgumentNullException.ThrowIfNull(entities, nameof(entities));

    if (string.IsNullOrWhiteSpace(fieldsString))
      return entities;

    var fields = fieldsString.Split(',');

    var shapedEntities = new List<ExpandoObject>();

    foreach (var entity in entities)
    {
      var dataShapedObject = new ExpandoObject();

      foreach (var field in fields)
      {
        var propertyInfo = typeof(T)
          .GetProperty(field.Trim(),
            BindingFlags.IgnoreCase
            | BindingFlags.Public
            | BindingFlags.Instance);

        if (propertyInfo is null)
          continue;

        var propertyValue = propertyInfo.GetValue(entity);
        (dataShapedObject as IDictionary<string, object>)
          .Add(propertyInfo.Name, propertyValue ?? string.Empty);
      }

      shapedEntities.Add(dataShapedObject);
    }

    return (IEnumerable<T>)shapedEntities;
  }

  public Task<int> CountAsync(CancellationToken cancellationToken = default)
    => _dbSet.CountAsync(cancellationToken);

  public Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    => _dbSet.AnyAsync(cancellationToken);
}
