namespace Profio.Infrastructure.Message;

public interface IMessageService
{
  string? GetUserInfo();
  string SendSms(string[] phones, string content, string sender);
  string SendMms(string[] phones, string content, string link, string sender);
}
