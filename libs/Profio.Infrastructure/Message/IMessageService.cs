namespace Profio.Infrastructure.Message;

public interface IMessageService
{
  string? GetUserInfo();
  Task<string>SendSms(string phones, string content);
}
