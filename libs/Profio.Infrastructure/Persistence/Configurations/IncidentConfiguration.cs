using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profio.Domain.Entities;

namespace Profio.Infrastructure.Persistence.Configurations;

public class IncidentConfiguration : IEntityTypeConfiguration<Incident>
{
  public void Configure(EntityTypeBuilder<Incident> builder)
  {
    builder.HasKey(e => e.Id);

    builder.Property(e => e.Id)
      .HasMaxLength(26);

    builder.Property(e => e.Description)
      .HasMaxLength(250)
      .IsUnicode();

    builder.Property(e => e.Status);

    builder.Property(e => e.Time);

    builder.HasOne(e => e.OrderHistory)
      .WithMany(e => e.Incidents)
      .HasForeignKey(e => e.OrderHistoryId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
