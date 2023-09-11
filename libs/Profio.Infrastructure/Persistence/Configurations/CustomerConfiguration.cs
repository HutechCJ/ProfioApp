using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profio.Domain.Entities;

namespace Profio.Infrastructure.Persistence.Configurations;

public sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
  public void Configure(EntityTypeBuilder<Customer> builder)
  {
    builder.HasKey(e => e.Id);

    builder.Property(e => e.Id)
      .HasMaxLength(26);

    builder.Property(e => e.Name)
      .HasMaxLength(50)
      .IsUnicode()
      .IsRequired();

    builder.Property(e => e.Phone)
      .HasMaxLength(10)
      .IsFixedLength()
      .IsRequired();

    builder.Property(e => e.Email)
      .HasMaxLength(50)
      .IsRequired();

    builder.Property(e => e.Gender);

    builder.Property(e => e.Address)
      .HasColumnType("jsonb")
      .IsUnicode();
  }
}
