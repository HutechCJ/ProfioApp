using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Profio.Infrastructure.Persistence.Converter;

public sealed class TimeOnlyConverter : ValueConverter<TimeOnly, TimeSpan>
{
  public TimeOnlyConverter() : base(
    timeOnly => timeOnly.ToTimeSpan(),
    timeSpan => TimeOnly.FromTimeSpan(timeSpan))
  {
  }
}
