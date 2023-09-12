using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profio.Domain.Entities;
using Profio.Infrastructure.Persistence.Converter;

namespace Profio.Infrastructure.Persistence.Configurations;

public sealed class DeliveryProgressConfiguration : IEntityTypeConfiguration<DeliveryProgress>
{
  public void Configure(EntityTypeBuilder<DeliveryProgress> builder)
  {
    builder.HasKey(e => e.Id);

    builder.Property(e => e.Id)
      .HasMaxLength(26);

    builder.Property(e => e.CurrentLocation)
      .HasColumnType("jsonb")
      .IsUnicode();

    builder.Property(e => e.PercentComplete);

    builder.Property(e => e.EstimatedTimeRemaining)
      .HasConversion<TimeSpanConverter>();

    builder.Property(e => e.LastUpdated);

    builder.HasOne(e => e.Order)
      .WithMany(e => e.DeliveryProgresses)
      .HasForeignKey(e => e.OrderId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
