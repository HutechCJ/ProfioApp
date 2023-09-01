using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profio.Domain.Entities;

namespace Profio.Infrastructure.Persistence.Configurations;

public class DeliveryConfiguration : IEntityTypeConfiguration<Delivery>
{
  public void Configure(EntityTypeBuilder<Delivery> builder)
  {
    builder.HasKey(e => e.Id);

    builder.Property(e => e.Id)
      .HasMaxLength(26);

    builder.Property(e => e.DeliveryDate);

    builder.HasOne(e => e.Order)
      .WithMany(e => e.Deliveries)
      .HasForeignKey(e => e.OrderId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(e => e.Vehicle)
      .WithMany(e => e.Deliveries)
      .HasForeignKey(e => e.VehicleId)
      .OnDelete(DeleteBehavior.SetNull);
  }
}
