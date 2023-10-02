namespace Profio.Domain.Interfaces;

public interface IEntity<out T> where T : notnull
{
  T Id { get; }
}

public interface IEntity : IEntity<string>
{
}
