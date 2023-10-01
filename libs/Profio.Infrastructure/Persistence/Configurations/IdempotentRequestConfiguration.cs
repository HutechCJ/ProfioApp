using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Profio.Infrastructure.Persistence.Configurations;

public sealed class IdempotentRequestConfiguration : IEntityTypeConfiguration<IdempotentRequest>
{
  public void Configure(EntityTypeBuilder<IdempotentRequest> builder)
  {
    builder.HasKey(x => x.Id);
    builder.Property(x => x.Name).IsRequired();
  }
}
