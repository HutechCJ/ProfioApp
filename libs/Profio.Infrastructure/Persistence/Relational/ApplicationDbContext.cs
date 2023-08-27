using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Profio.Infrastructure.Identity;

namespace Profio.Infrastructure.Persistence.Relational;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDatabaseFacade
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
  {

  }
}
