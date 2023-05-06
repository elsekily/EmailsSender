using EmailSender.Entities.Models;
using EmailSender.Entities.Resources;
using MimeKit;

namespace EmailSender.Core;

public interface IEmailService
{
    Task<bool> SendEmail(MimeMessage email);
}
