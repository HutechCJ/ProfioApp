using FluentEmail.Core;

namespace Profio.Infrastructure.Email.FluentEmail.Internal;

public sealed class EmailService : IEmailService
{
  private readonly IFluentEmailFactory _fluentEmailFactory;

  public EmailService(IFluentEmailFactory fluentEmailFactory)
    => _fluentEmailFactory = fluentEmailFactory;

  public async Task SendEmailAsync(EmailMetadata metadata)
    => await _fluentEmailFactory
      .Create()
      .To(metadata.To)
      .Subject(metadata.Subject)
      .UsingTemplateFromFile(metadata.Template, metadata.Model)
      .SendAsync();
}
