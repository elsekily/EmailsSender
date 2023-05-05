using EmailSender.Core;
using EmailSender.Entities.Models;
using EmailSender.Entities.Resources;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

public class EmailService : IEmailService
{
    private readonly IConfiguration configuration;

    public EmailService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public void SendEmail(EmailResource emailResource)
    {
        var email = BuildMimeMessage(emailResource);
        SendEmailWithSmtpClient(email);
    }

    private MimeMessage BuildMimeMessage(EmailResource emailResource)
    {
        var email = new MimeMessage();
        System.Console.WriteLine(configuration["emailConfiguration:Username"]);
        email.From.Add(MailboxAddress.Parse(configuration["emailConfiguration:Username"]));
        email.To.AddRange(emailResource.EmailAddresses.Select(e => new MailboxAddress("", e)));
        email.Subject = emailResource.Message.Subject;
        email.Body = new TextPart(TextFormat.Plain) { Text = emailResource.Message.Body };
        return email;
    }

    private void SendEmailWithSmtpClient(MimeMessage email)
    {
        using (var smtp = new SmtpClient())
        {
            smtp.Connect(configuration["emailConfiguration:SmtpServer"],
                    int.Parse(configuration["emailConfiguration:Port"]),
                    SecureSocketOptions.StartTls);

            smtp.Authenticate(configuration["emailConfiguration:Username"],
                    configuration["emailConfiguration:Password"]);

            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}