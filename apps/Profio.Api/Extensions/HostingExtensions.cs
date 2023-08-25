using Profio.Application;

namespace Profio.Api.Extensions;

public static class HostingExtensions
{
  public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
  {
    builder.Services.AddApplicationServices();
    //builder.Services.AddInfrastructureServices(builder.Configuration);
    //builder.Services.AddWebApiServices(builder.Configuration);

    return builder.Build();
  }
  public static async Task<WebApplication> ConfigurePipelineAsync(this WebApplication app)
  {

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    return app;
  }
}
