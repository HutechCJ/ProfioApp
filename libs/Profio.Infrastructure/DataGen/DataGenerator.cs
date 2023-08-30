using Profio.Domain.Entities;
using Profio.Infrastructure.DataGen.Fakers;

namespace Profio.Infrastructure.DataGen;

public sealed class DataGenerator
{
  public static List<Staff> Staffs { get; set; } = new();

  public static void InitBogusData()
  {
    var staffs = StaffFaker.GetStaffGenerator().Generate(10);
  }
}
