using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Profio.Infrastructure.Persistence.Relational.Converter;

public class TimeOnlyConverter : ValueConverter<TimeOnly, TimeSpan>
{
    public TimeOnlyConverter() : base(
        timeOnly => timeOnly.ToTimeSpan(),
        timeSpan => TimeOnly.FromTimeSpan(timeSpan))
    { }
}
