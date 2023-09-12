using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Profio.Infrastructure.Message;

public static class Extension
{
  public static void AddMessageService(this IServiceCollection services, IConfiguration configuration)
  {
    var token = configuration["Sms:AccessToken"];
    services.AddSingleton<IMessageService>(new MessageService(token));
  }
}
