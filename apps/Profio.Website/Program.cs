using CurrieTechnologies.Razor.SweetAlert2;
using Profio.Infrastructure.OpenTelemetry;
using Profio.Website.Data.Customers;
using Profio.Website.Middleware;
using Serilog;
using Spectre.Console;

AnsiConsole.Write(new FigletText("Profio Website").Centered().Color(Color.BlueViolet));

try
{
  var builder = WebApplication.CreateBuilder(args);

  builder.Services.AddRazorPages();
  builder.Services.AddServerSideBlazor();
  builder.Services.AddSweetAlert2();
  builder.Services.AddHttpClient("Profio Api", config =>
  {
    config.BaseAddress = new Uri(builder.Configuration["ApiUrl"] ?? "https://localhost:9023/api/v1");
  });
  builder.AddOpenTelemetry();

  builder.Services.AddSingleton<ICustomerService, CustomerService>();

  var app = builder.Build();

  if (!app.Environment.IsDevelopment())
  {
    app.UseExceptionHandler("/Error");
    app.UseHsts();
  }

  app.UseRobotsTxtMiddleware();
  app.UseHttpsRedirection();
  app.UseStaticFiles();
  app.UseRouting();
  app.MapBlazorHub();
  app.MapFallbackToPage("/_Host");

  app.Run();
}
catch (Exception ex)
  when (ex.GetType().Name is not "StopTheHostException"
        && ex.GetType().Name is not "HostAbortedException")
{
  Log.Fatal(ex, "Unhandled exception");
}
finally
{
  Log.Information("Shut down complete");
  Log.CloseAndFlush();
}
