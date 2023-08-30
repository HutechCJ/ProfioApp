using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profio.Domain.Entities;

namespace Profio.Infrastructure.Persistence.Configurations;

public class RouteConfiguration : IEntityTypeConfiguration<Route>
{
  public void Configure(EntityTypeBuilder<Route> builder)
  {
    builder.HasKey(e => e.Id);

    builder.Property(e => e.Id)
      .HasMaxLength(26);

    builder.Property(e => e.Distance);

    builder.HasOne(e => e.StartHub)
      .WithMany(e => e.StartRoutes)
      .HasForeignKey(e => e.StartHubId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(e => e.EndHub)
      .WithMany(e => e.EndRoutes)
      .HasForeignKey(e => e.EndHubId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
