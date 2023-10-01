namespace Profio.Infrastructure;

public static class ConfigureServices
{
  public static void AddInfrastructureServices(this IServiceCollection services, WebApplicationBuilder builder)
  {
    services.AddControllers(options =>
      {
        options.RespectBrowserAcceptHeader = true;
        options.ReturnHttpNotAcceptable = true;
        options.Filters.Add<LoggingFilter>();
        options.Filters.Add<ExceptionFilter>();
      })
      .AddNewtonsoftJson(options =>
        options.SerializerSettings.ContractResolver = new DefaultContractResolver()
        {
          NamingStrategy = new CamelCaseNamingStrategy()
          {
            ProcessDictionaryKeys = true
          }
        })
      .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
      )
      .AddApplicationPart(AssemblyReference.Assembly);

    services.AddResponseCompression(options =>
      {
        options.EnableForHttps = true;
        options.Providers.Add<GzipCompressionProvider>();
        options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
        {
          MediaTypeNames.Application.Json,
          MediaTypeNames.Text.Plain,
          MediaTypeNames.Image.Jpeg
        });
      })
      .Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal)
      .AddResponseCaching(options => options.MaximumBodySize = 1024)
      .AddRouting(options => options.LowercaseUrls = true);

    services.Configure<FormOptions>(options =>
      options.MultipartBodyLengthLimit = 50000000
    );

    services.AddCors(options => options
      .AddDefaultPolicy(policy => policy
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()));

    builder.AddApiVersioning();
    builder.AddSerilog("Profio Api");
    builder.AddOpenTelemetry();
    builder.AddHealthCheck();
    builder.AddSocketHub();
    builder.AddLuceneSearch();
    builder.AddHttpRestClient();

    builder.AddBackgroundJob();

    services
      .AddProblemDetails()
      .AddEndpointsApiExplorer()
      .AddOpenApi();

    services.AddSingleton<IDeveloperPageExceptionFilter, DeveloperPageExceptionFilter>();
    services.AddScoped<ITokenService, TokenService>();
    services.AddScoped<IIdempotencyService, IdempotencyService>();
    services.AddHttpClient<ITwilioRestClient, TwilioClient>();

    services.AddScoped<ClientIpCheckActionFilter>(container => new(
      container.GetRequiredService<ILogger<ClientIpCheckActionFilter>>(), builder.Configuration["AdminSafeList"]));

    services.AddPostgres(builder.Configuration)
      .AddRedisCache(builder.Configuration)
      .AddEmailSender(builder.Configuration)
      .AddEventBus(builder.Configuration);

    services.AddMqttBus(builder.Configuration);
    services.AddStorage(builder.Configuration);

    services.AddApplicationIdentity(builder, builder.Configuration);

    services.AddApiKey();
  }

  public static async Task UseWebInfrastructureAsync(this WebApplication app)
  {
    if (app.Environment.IsDevelopment())
    {
      using var scope = app.Services.CreateScope();
      var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
      await initializer.InitialiseAsync();
      await initializer.SeedAsync();
    }

    app
      .UseAuthentication()
      .UseAuthorization();

    app
      .UseMiddleware<TimeOutMiddleware>()
      .UseMiddleware<XssProtectionMiddleware>();

    app.MapLocationHub();

    app
      .UseCors()
      .UseExceptionHandler()
      .UseHttpsRedirection()
      .UseCookiePolicy()
      .UseRateLimiter()
      .UseResponseCaching()
      .UseResponseCompression()
      .UseStatusCodePages()
      .UseStaticFiles();

    app.MapHealthCheck();
    app.Map("/", () => Results.Redirect("/swagger"));
    app.Map("/error", () => Results.Problem("An unexpected error occurred.", statusCode: StatusCodes.Status500InternalServerError))
      .ExcludeFromDescription();
  }
}
