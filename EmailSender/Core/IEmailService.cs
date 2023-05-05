using EmailSender.Entities.Models;
using EmailSender.Entities.Resources;

namespace EmailSender.Core;

public interface IEmailService
{
    void SendEmail(EmailResource email);
}
