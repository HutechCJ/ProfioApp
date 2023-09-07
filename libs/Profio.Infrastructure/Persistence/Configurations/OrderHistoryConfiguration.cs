using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profio.Domain.Entities;

namespace Profio.Infrastructure.Persistence.Configurations;

public class OrderHistoryConfiguration : IEntityTypeConfiguration<OrderHistory>
{
  public void Configure(EntityTypeBuilder<OrderHistory> builder)
  {
    builder.HasKey(e => e.Id);

    builder.Property(e => e.Id)
      .HasMaxLength(26);

    builder.Property(e => e.Timestamp);

    builder.HasOne(e => e.Hub)
      .WithMany(e => e.OrderHistories)
      .HasForeignKey(e => e.HubId)
      .OnDelete(DeleteBehavior.SetNull);

    builder.HasOne(e => e.Delivery)
      .WithMany(e => e.OrderHistories)
      .HasForeignKey(e => e.DeliveryId)
      .OnDelete(DeleteBehavior.SetNull);
  }
}
