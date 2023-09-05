using Bogus;
using Profio.Domain.Constants;
using Profio.Domain.Entities;

namespace Profio.Infrastructure.DataGen.Fakers;

public class StaffFaker
{
  public static Faker<Staff> GetStaffGenerator()
    => new Faker<Staff>()
      .RuleFor(x => x.Id, _ => Ulid.NewUlid().ToString())
      .RuleFor(x => x.Name, f => f.Name.FullName())
      .RuleFor(x => x.Phone, f => f.Phone.PhoneNumber())
      .RuleFor(x => x.Position, f => f.PickRandom<Position>());
}
