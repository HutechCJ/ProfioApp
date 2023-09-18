using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profio.Domain.Identity;

namespace Profio.Infrastructure.Persistence.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
  public void Configure(EntityTypeBuilder<ApplicationUser> builder)
  {
    builder.HasOne(e => e.Staff)
      .WithMany(e => e.Users)
      .HasForeignKey(e => e.StaffId)
      .OnDelete(DeleteBehavior.SetNull);
  }
}
