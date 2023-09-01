using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Profio.Infrastructure.Persistence;
using SameSiteMode = Microsoft.AspNetCore.Http.SameSiteMode;

namespace Profio.Infrastructure.Identity;

public static class Extensions
{
  public static void AddApplicationIdentity(this IServiceCollection services, WebApplicationBuilder builder)
  {
    services.AddIdentityCore<ApplicationUser>(options =>
    {
      options.Password.RequireDigit = true;
      options.Password.RequireLowercase = true;
      options.Password.RequireNonAlphanumeric = true;
      options.Password.RequireUppercase = true;
      options.Password.RequiredLength = 6;
      options.Password.RequiredUniqueChars = 1;

      options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
      options.Lockout.MaxFailedAccessAttempts = 5;
      options.Lockout.AllowedForNewUsers = true;

      options.User.RequireUniqueEmail = true;
    })
      .AddEntityFrameworkStores<ApplicationDbContext>()
      .AddDefaultTokenProviders();

    services.AddAntiforgery(options =>
    {
      options.Cookie.Name = "XSRF-TOKEN";
      options.Cookie.HttpOnly = true;
      options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
      options.HeaderName = "X-XSRF-TOKEN";
    });

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:TokenKey"] ?? string.Empty));

    services.AddAuthentication(
        options =>
        {
          options.DefaultScheme = IdentityConstants.ApplicationScheme;
          options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
        })
      .AddJwtBearer(options =>
        options.TokenValidationParameters = new()
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = key,
          ValidateIssuer = false,
          ValidateAudience = false,
          ValidateLifetime = true,
          ClockSkew = TimeSpan.FromSeconds(5)
        }
      )
      .AddCookie(options =>
      {
        options.Cookie.Name = "USER-TOKEN";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.ExpireTimeSpan = TimeSpan.FromDays(3);
        options.SlidingExpiration = true;
      })
      .AddPolicyScheme(
        IdentityConstants.ApplicationScheme,
        IdentityConstants.ApplicationScheme,
        options =>
      {
        options.ForwardDefaultSelector = context =>
        {
          var authHeader = context.Request.Headers[HeaderNames.Authorization].FirstOrDefault();
          if (authHeader?.StartsWith("Bearer ") ?? false)
            return JwtBearerDefaults.AuthenticationScheme;
          return CookieAuthenticationDefaults.AuthenticationScheme;
        };
      });
  }
}
