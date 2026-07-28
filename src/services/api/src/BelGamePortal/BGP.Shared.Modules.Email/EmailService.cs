using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace BGP.Shared.Modules.Email;

public class EmailService : IEmailService
{
    private readonly IEmailConfiguration _emailConfiguration;

    public EmailService(IEmailConfiguration emailConfiguration)
    {
        _emailConfiguration = emailConfiguration;
    }

    public async Task SendAsync()
    {
        var mailMessage = new MimeMessage();
        mailMessage.From.Add(new MailboxAddress("from name", this._emailConfiguration.UserName));
        mailMessage.To.Add(new MailboxAddress("to name", "alaznick@gmail.com"));
        mailMessage.Subject = "subject";
        mailMessage.Body = new TextPart("plain")
        {
            Text = "Hello"
        };

        using var smtpClient = new SmtpClient();

        await smtpClient.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
        await smtpClient.AuthenticateAsync(this._emailConfiguration.UserName, this._emailConfiguration.Password);
        await smtpClient.SendAsync(mailMessage);
        await smtpClient.DisconnectAsync(true);

    }
}
