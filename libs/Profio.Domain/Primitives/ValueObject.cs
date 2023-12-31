namespace Profio.Domain.Primitives;

public abstract class ValueObject : IEquatable<ValueObject>
{
  public bool Equals(ValueObject? other)
    => other is { } && ValueAreEqual(other);

  protected abstract IEnumerable<object?> GetEqualityComponents();

  public override bool Equals(object? obj)
    => obj is ValueObject other && ValueAreEqual(other);

  public override int GetHashCode()
    => GetEqualityComponents().Aggregate(default(int), HashCode.Combine);

  private bool ValueAreEqual(ValueObject other)
    => GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());

  public static bool operator ==(ValueObject? left, ValueObject? right)
    => Equals(left, right);

  public static bool operator !=(ValueObject? left, ValueObject? right)
    => !Equals(left, right);
}
