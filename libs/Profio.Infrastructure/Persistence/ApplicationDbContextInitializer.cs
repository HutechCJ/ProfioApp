using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Profio.Infrastructure.Identity;
using Serilog;

namespace Profio.Infrastructure.Persistence;

public class ApplicationDbContextInitializer
{
  private readonly ApplicationDbContext _context;
  private readonly UserManager<ApplicationUser> _userManager;

  public ApplicationDbContextInitializer(
    ApplicationDbContext context,
    UserManager<ApplicationUser> userManager)
    => (_context, _userManager) = (context, userManager);

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
    if (_userManager.Users.Any())
      return;

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

    var loi = new ApplicationUser
    {
      UserName = "loi@gmail.com",
      FullName = "Tạ Thạch Lỗi",
      Email = "loi@gmail.com"
    };

    const string password = "P@ssw0rd";

    await Task.WhenAll(
      _userManager.CreateAsync(thai, password),
      _userManager.CreateAsync(nhon, password),
      _userManager.CreateAsync(nhan, password),
      _userManager.CreateAsync(dat, password),
      _userManager.CreateAsync(loi, password)
    ).ConfigureAwait(false);
  }
}
