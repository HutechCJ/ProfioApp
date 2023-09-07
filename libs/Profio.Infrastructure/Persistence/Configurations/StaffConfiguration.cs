using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profio.Domain.Entities;

namespace Profio.Infrastructure.Persistence.Configurations;

public class StaffConfiguration : IEntityTypeConfiguration<Staff>
{
  public void Configure(EntityTypeBuilder<Staff> builder)
  {
    builder.HasKey(e => e.Id);

    builder.Property(e => e.Id)
      .HasMaxLength(26);

    builder.Property(e => e.Name)
      .HasMaxLength(50)
      .IsUnicode()
      .IsRequired();

    builder.Property(e => e.Phone)
      .HasMaxLength(10)
      .IsFixedLength()
      .IsRequired();

    builder.Property(e => e.Position)
      .IsRequired();
  }
}
