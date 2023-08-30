using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Profio.Infrastructure.Persistence.Converter;

public class TimeSpanConverter : ValueConverter<TimeSpan, string>
{
  public TimeSpanConverter()
    : base(v => v.ToString(), x => TimeSpan.Parse(x ?? TimeSpan.Zero.ToString()))
  {
  }
}
