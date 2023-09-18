using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Profio.Domain.Entities;
using Profio.Domain.Identity;
using Profio.Infrastructure.Persistence.Idempotency;

namespace Profio.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDatabaseFacade
{
  public DbSet<Customer> Customers { get; set; } = default!;
  public DbSet<Delivery> Deliveries { get; set; } = default!;
  public DbSet<DeliveryProgress> DeliveryProgresses { get; set; } = default!;
  public DbSet<Domain.Entities.Hub> Hubs { get; set; } = default!;
  public DbSet<Incident> Incidents { get; set; } = default!;
  public DbSet<Order> Orders { get; set; } = default!;
  public DbSet<Phase> Phases { get; set; } = default!;
  public DbSet<Route> Routes { get; set; } = default!;
  public DbSet<Staff> Staffs { get; set; } = default!;
  public DbSet<Vehicle> Vehicles { get; set; } = default!;
  public DbSet<IdempotentRequest> IdempotentRequests { get; set; } = default!;

  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);
    builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
  }
}
