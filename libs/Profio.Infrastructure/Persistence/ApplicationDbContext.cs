using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Profio.Infrastructure.Identity;

namespace Profio.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
}
