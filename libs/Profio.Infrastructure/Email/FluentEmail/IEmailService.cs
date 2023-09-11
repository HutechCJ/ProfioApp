using Profio.Infrastructure.Email.FluentEmail.Internal;

namespace Profio.Infrastructure.Email.FluentEmail;

public interface IEmailService
{
  Task SendEmailAsync(EmailMetadata metadata);
}
