using Profio.Infrastructure.Email.FluentEmail.Internal;

namespace Profio.Infrastructure.Email.FluentEmail;

public interface IEmailService
{
  public Task SendEmailAsync(EmailMetadata metadata);
}
