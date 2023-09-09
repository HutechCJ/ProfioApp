using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profio.Infrastructure.Persistence.Idempotency;

namespace Profio.Infrastructure.Persistence.Configurations;

public class IdempotentRequestConfiguration : IEntityTypeConfiguration<IdempotentRequest>
{
  public void Configure(EntityTypeBuilder<IdempotentRequest> builder)
  {
    builder.HasKey(x => x.Id);
    builder.Property(x => x.Name).IsRequired();
  }
}
