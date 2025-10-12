using Microsoft.Extensions.Configuration;

namespace Alternance.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        // Implementation using SMTP or Email service provider (SendGrid, Mailgun, etc.)
        await Task.CompletedTask;
        
        // Example: Send email logic here
        Console.WriteLine($"Sending email to {to}: {subject}");
    }

    public async Task SendTemplatedEmailAsync(string to, string templateName, object data)
    {
        // Load and populate email template
        await Task.CompletedTask;
        
        Console.WriteLine($"Sending templated email ({templateName}) to {to}");
    }
}
