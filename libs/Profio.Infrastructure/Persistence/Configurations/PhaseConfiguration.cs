using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profio.Domain.Entities;

namespace Profio.Infrastructure.Persistence.Configurations;

public sealed class PhaseConfiguration : IEntityTypeConfiguration<Phase>
{
  public void Configure(EntityTypeBuilder<Phase> builder)
  {
    builder.HasKey(e => e.Id);

    builder.Property(e => e.Id)
      .HasMaxLength(26);

    builder.Property(e => e.Order);

    builder.HasOne(e => e.Route)
      .WithMany(e => e.Phases)
      .HasForeignKey(e => e.RouteId)
      .OnDelete(DeleteBehavior.SetNull);
  }
}
