using Serilog;

try
{
  var builder = WebApplication.CreateBuilder(args);

  builder.Services.AddRazorPages();
  builder.Services.AddServerSideBlazor();

  var app = builder.Build();

  if (!app.Environment.IsDevelopment())
  {
    app.UseExceptionHandler("/Error");
    app.UseHsts();
  }

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
