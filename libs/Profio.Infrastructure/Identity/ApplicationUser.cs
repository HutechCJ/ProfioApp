using Microsoft.AspNetCore.Identity;
using Profio.Domain.Interfaces;

namespace Profio.Infrastructure.Identity;

public class ApplicationUser : IdentityUser, IEntity<string>
{
}
