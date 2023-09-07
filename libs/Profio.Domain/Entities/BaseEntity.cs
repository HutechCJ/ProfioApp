using Profio.Domain.Interfaces;

namespace Profio.Domain.Entities;

public class BaseEntity<T> : IEntity<T>
  where T : notnull
{

  public T Id { get; set; } = default!;
}

public class BaseEntity : BaseEntity<string>
{
  public BaseEntity()
  {
    Id = Ulid.NewUlid().ToString();
  }
}
