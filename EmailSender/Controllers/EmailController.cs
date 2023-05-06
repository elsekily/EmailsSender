using EmailSender.Core;
using EmailSender.Entities.Models;
using EmailSender.Entities.Resources;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace EmailSender.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly IEmailService emailService;
    private readonly IMessageRepository repository;
    private readonly IConfiguration configuration;

    public EmailController(IEmailService emailService, IMessageRepository messageRepository, IConfiguration configuration)
    {
        this.emailService = emailService;
        this.repository = messageRepository;
        this.configuration = configuration;
    }
    [HttpPost]
    public async Task<IActionResult> SendEmail([FromBody] MessageRecipientsResource messageRecipients)
    {
        var message = await repository.GetMessage(messageRecipients.MessageId.Value);

        if (message == null)
            return BadRequest();

        var mimeMessage = BuildMimeMessage(messageRecipients, message);
        var sendResult = await emailService.SendEmail(mimeMessage);

        if (!sendResult)
            return StatusCode(503, "Service Unavailable. Please try again later.");

        return Ok();
    }

    private MimeMessage BuildMimeMessage(MessageRecipientsResource messageRecipients, Message message)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(configuration["emailConfiguration:Username"]));
        email.To.AddRange(messageRecipients.RecipientEmailAddresses.Select(e => new MailboxAddress("", e)));
        email.Subject = message.Subject;
        email.Body = new TextPart(TextFormat.Plain) { Text = message.Body };
        return email;
    }
}