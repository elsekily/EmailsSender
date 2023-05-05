using EmailSender.Entities.Models;

namespace EmailSender.Entities.Resources;

public class EmailResource
{
    public Message Message { get; set; }
    public IList<string> EmailAddresses { get; set; }
}