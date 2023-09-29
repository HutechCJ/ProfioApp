using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Profio.Domain.Identity;
using Profio.Infrastructure.Persistence;
using System.Text;

namespace Profio.Infrastructure.Auth;

public static class Extensions
{
  public static void AddApplicationIdentity(
    this IServiceCollection services,
    WebApplicationBuilder builder,
    IConfiguration configuration)
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
      .AddRoles<IdentityRole>()
      .AddEntityFrameworkStores<ApplicationDbContext>()
      .AddDefaultTokenProviders();

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:TokenKey"] ?? string.Empty));

    services.AddAuthentication(
        options =>
        {
          options.DefaultScheme = IdentityConstants.ApplicationScheme;
          options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
      .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
        options =>
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
      .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
        options =>
        {
          options.Cookie.Name = "USER-TOKEN";
          options.Cookie.HttpOnly = true;
          options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
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
            return authHeader?.StartsWith("Bearer ") == true
              ? JwtBearerDefaults.AuthenticationScheme
              : CookieAuthenticationDefaults.AuthenticationScheme;
          };
        });

    //if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
    //{
    //  services.AddAuthentication()
    //    .AddOpenIdConnect(options =>
    //  {
    //    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //    options.Authority = configuration.GetSection("OAuth2")["ServerRealm"];
    //    options.ClientId = configuration.GetSection("OAuth2")["ClientId"];
    //    options.ClientSecret = configuration.GetSection("OAuth2")["ClientSecret"];
    //    options.MetadataAddress = configuration.GetSection("OAuth2")["Metadata"];
    //    options.RequireHttpsMetadata = true;
    //    options.GetClaimsFromUserInfoEndpoint = true;
    //    options.Scope.Add("openid");
    //    options.Scope.Add("profile");
    //    options.SaveTokens = true;
    //    options.ResponseType = OpenIdConnectResponseType.Code;
    //  });
    //}

    services.AddScoped<IUserAccessor, UserAccessor>();
  }
}
