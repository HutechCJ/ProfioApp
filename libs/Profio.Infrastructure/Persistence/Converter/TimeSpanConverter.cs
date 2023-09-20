using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Globalization;

namespace Profio.Infrastructure.Persistence.Converter;

public sealed class TimeSpanConverter : ValueConverter<TimeSpan, string>
{
  public TimeSpanConverter()
    : base(v => v.ToString(), x => TimeSpan.Parse(x, new CultureInfo("vi-VN")))
  {
  }
}
