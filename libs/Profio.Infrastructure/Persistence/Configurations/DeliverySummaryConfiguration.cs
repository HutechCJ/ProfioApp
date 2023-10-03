using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profio.Domain.Views;

namespace Profio.Infrastructure.Persistence.Configurations;

public sealed class DeliverySummaryConfiguration : IEntityTypeConfiguration<DeliverySummary>
{
  public void Configure(EntityTypeBuilder<DeliverySummary> builder)
  {
    builder.HasKey(x => x.Id);

    builder.Property(x => x.Orders).HasColumnType("jsonb");
  }
}
