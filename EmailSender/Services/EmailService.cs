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
    public async Task<bool> SendEmail(MimeMessage email)
    {
        using (var smtp = new SmtpClient())
        {
            try
            {
                await smtp.ConnectAsync(configuration["emailConfiguration:SmtpServer"],
                                        int.Parse(configuration["emailConfiguration:Port"]),
                                        SecureSocketOptions.StartTls);

                await smtp.AuthenticateAsync(configuration["emailConfiguration:Username"],
                                       configuration["emailConfiguration:Password"]);

                await smtp.SendAsync(email);

                return true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Error : {0}", e);
                return false;
            }
            finally
            {
                smtp.Disconnect(true);
            }
        }
    }
}