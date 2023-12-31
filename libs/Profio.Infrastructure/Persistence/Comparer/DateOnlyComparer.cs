using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Profio.Infrastructure.Persistence.Comparer;

public sealed class DateOnlyComparer : ValueComparer<DateOnly>
{
  public DateOnlyComparer() : base(
    (d1, d2) => d1.DayNumber == d2.DayNumber,
    d => d.GetHashCode())
  {
  }
}
