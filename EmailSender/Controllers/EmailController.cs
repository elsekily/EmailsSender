using AutoMapper;
using EmailSender.Core;
using EmailSender.Entities.Models;
using EmailSender.Entities.Resources;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly IEmailService emailService;
    private readonly IMessageRepository repository;

    public EmailController(IEmailService emailService, IMessageRepository messageRepository)
    {
        this.emailService = emailService;
        this.repository = messageRepository;
    }
    [HttpPost]
    public async Task<IActionResult> SendEmail([FromBody] MessageRecipientsResource messageRecipients)
    {
        var message = await repository.GetMessage(messageRecipients.MessageId.Value);

        if (message == null)
            return BadRequest();

        var email = BuildEmailResource(messageRecipients, message);

        emailService.SendEmail(email);
        return Ok();
    }

    private EmailResource BuildEmailResource(MessageRecipientsResource messageRecipients, Message message)
    {
        return new EmailResource()
        {
            Message = message,
            EmailAddresses = messageRecipients.RecipientEmailAddresses
        };
    }
}