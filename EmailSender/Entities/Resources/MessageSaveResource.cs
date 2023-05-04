using System.ComponentModel.DataAnnotations;

namespace EmailSender.Entities.Resources;

public class MessageSaveResource
{
    [Required]
    [MaxLength(255)]
    public string Subject { get; set; }
    [Required]
    public string Body { get; set; }
}