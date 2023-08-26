using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Profio.Infrastructure.Identity;

namespace Profio.Infrastructure.Persistence.Relational;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
}
