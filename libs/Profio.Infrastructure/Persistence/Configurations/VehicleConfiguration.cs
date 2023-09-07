using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profio.Domain.Entities;

namespace Profio.Infrastructure.Persistence.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
  public void Configure(EntityTypeBuilder<Vehicle> builder)
  {
    builder.HasKey(e => e.Id);

    builder.Property(e => e.Id)
      .HasMaxLength(26);

    builder.Property(e => e.ZipCodeCurrent)
      .HasMaxLength(50);

    builder.Property(e => e.LicensePlate)
      .HasMaxLength(50);

    builder.Property(e => e.Type);

    builder.Property(e => e.Status);

    builder.HasOne(e => e.Staff)
      .WithMany(e => e.Vehicles)
      .HasForeignKey(e => e.StaffId)
      .OnDelete(DeleteBehavior.SetNull);
  }
}
