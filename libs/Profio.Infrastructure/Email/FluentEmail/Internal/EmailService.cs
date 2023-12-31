using FluentEmail.Core;
using Microsoft.Extensions.Logging;
using NetCore.AutoRegisterDi;
using Polly;

namespace Profio.Infrastructure.Email.FluentEmail.Internal;

[RegisterAsScoped]
public sealed class EmailService : IEmailService
{
  private readonly IFluentEmailFactory _fluentEmailFactory;
  private readonly ILogger<EmailService> _logger;

  public EmailService(IFluentEmailFactory fluentEmailFactory, ILogger<EmailService> logger)
    => (_fluentEmailFactory, _logger) = (fluentEmailFactory, logger);

  public async Task SendEmailAsync(EmailMetadata metadata)
    => await Policy
      .Handle<Exception>()
      .WaitAndRetryAsync(
        3,
        _ => TimeSpan.FromMilliseconds(new Random().Next(1000, 3000)),
        (_, retryCount, _) =>
          _logger.LogWarning("Failed to send email. Retrying... Attempt: {retryCount}", retryCount)
      ).ExecuteAsync(async () =>
        await _fluentEmailFactory
          .Create()
          .To(metadata.To)
          .Subject(metadata.Subject)
          .UsingTemplateFromFile(metadata.Template, metadata.Model)
          .SendAsync()
      );
}
