using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Profio.Domain.Constants;
using Profio.Domain.Identity;
using Serilog;

namespace Profio.Infrastructure.Persistence;

public class ApplicationDbContextInitializer
{
  private readonly ApplicationDbContext _context;
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly RoleManager<IdentityRole> _roleManager;

  public ApplicationDbContextInitializer(
    ApplicationDbContext context,
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager)
    => (_context, _userManager, _roleManager) = (context, userManager, roleManager);

  public async Task InitialiseAsync()
  {
    try
    {
      await _context.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
      Log.Error(ex, "Migration error");
    }
  }

  public async Task SeedAsync()
  {
    try
    {
      await TrySeedAsync();
    }
    catch (Exception ex)
    {
      Log.Error(ex, "Seeding error");
    }
  }

  public async Task TrySeedAsync()
  {
    if (_userManager.Users.Any() || _roleManager.Roles.Any())
      return;

    var adminRole = new IdentityRole(UserRole.Admin);
    var driverRole = new IdentityRole(UserRole.Driver);
    var customerRole = new IdentityRole(UserRole.Customer);
    var officerRole = new IdentityRole(UserRole.Officer);
    var stokerRole = new IdentityRole(UserRole.Stoker);

    await _roleManager.CreateAsync(adminRole);
    await _roleManager.CreateAsync(driverRole);
    await _roleManager.CreateAsync(customerRole);
    await _roleManager.CreateAsync(officerRole);
    await _roleManager.CreateAsync(stokerRole);

    var thai = new ApplicationUser
    {
      UserName = "thai@gmail.com",
      FullName = "Nguyễn Hồng Thái",
      Email = "thai@gmail.com"
    };

    var nhon = new ApplicationUser
    {
      UserName = "nhon@gmail.com",
      FullName = "Võ Thương Trường Nhơn",
      Email = "nhon@gmail.com"
    };

    var nhan = new ApplicationUser
    {
      UserName = "nhan@gmail.com",
      FullName = "Nguyễn Xuân Nhân",
      Email = "nhan@gmail.com"
    };

    var dat = new ApplicationUser
    {
      UserName = "dat@gmail.com",
      FullName = "Hoàng Tiến Đạt",
      Email = "dat@gmail.com"
    };

    var van = new ApplicationUser
    {
      UserName = "van@gmail.com",
      FullName = "Trương Thục Vân",
      Email = "van@gmail.com"
    };

    const string password = "P@ssw0rd";

    await _userManager.CreateAsync(thai, password);
    await _userManager.CreateAsync(nhon, password);
    await _userManager.CreateAsync(nhan, password);
    await _userManager.CreateAsync(dat, password);
    await _userManager.CreateAsync(van, password);

    await _userManager.AddToRoleAsync(thai, UserRole.Admin);
    await _userManager.AddToRoleAsync(nhon, UserRole.Admin);
    await _userManager.AddToRoleAsync(nhan, UserRole.Admin);
    await _userManager.AddToRoleAsync(dat, UserRole.Admin);
    await _userManager.AddToRoleAsync(van, UserRole.Admin);
  }
}
