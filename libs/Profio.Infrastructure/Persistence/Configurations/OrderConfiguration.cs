using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profio.Domain.Entities;

namespace Profio.Infrastructure.Persistence.Configurations;

public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
  public void Configure(EntityTypeBuilder<Order> builder)
  {
    builder.HasKey(e => e.Id);

    builder.Property(e => e.Id)
      .HasMaxLength(26);

    builder.Property(e => e.StartedDate);

    builder.Property(e => e.ExpectedDeliveryTime);

    builder.Property(e => e.Status);

    builder.Property(e => e.DestinationZipCode)
      .HasMaxLength(50)
      .IsRequired();

    builder.Property(e => e.DestinationAddress)
      .HasColumnType("jsonb")
      .IsUnicode();

    builder.Property(e => e.Distance);

    builder.Property(e => e.Note)
      .HasMaxLength(250)
      .IsUnicode();

    builder.HasOne(e => e.Customer)
      .WithMany(e => e.Orders)
      .HasForeignKey(e => e.CustomerId)
      .OnDelete(DeleteBehavior.SetNull);
  }
}
