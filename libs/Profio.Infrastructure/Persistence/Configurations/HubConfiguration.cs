using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Profio.Infrastructure.Persistence.Configurations;

public class HubConfiguration : IEntityTypeConfiguration<Domain.Entities.Hub>
{
  public void Configure(EntityTypeBuilder<Domain.Entities.Hub> builder)
  {
    builder.HasKey(e => e.Id);

    builder.Property(e => e.Id)
      .HasMaxLength(26);

    builder.Property(e => e.Name)
      .HasMaxLength(50)
      .IsRequired();

    builder.Property(e => e.ZipCode)
      .HasMaxLength(50)
      .IsFixedLength()
      .IsRequired();

    builder.Property(e => e.Location)
      .HasColumnType("jsonb")
      .IsUnicode();

    builder.Property(e => e.Address)
      .HasColumnType("jsonb")
      .IsUnicode();

    builder.Property(e => e.Status);
  }
}
