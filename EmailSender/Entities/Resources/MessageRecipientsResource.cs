using System.ComponentModel.DataAnnotations;
using EmailSender.Attributes;

namespace EmailSender.Entities.Resources;

public class MessageRecipientsResource
{
    [Required]
    public int? MessageId { get; set; }

    [Required]
    [EmailListValidation]
    public IList<string> RecipientEmailAddresses { get; set; }
}
