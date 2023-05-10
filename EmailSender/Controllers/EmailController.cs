using AutoMapper;
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
    private readonly IMapper mapper;

    public EmailController(IEmailService emailService, IMessageRepository messageRepository, IMapper mapper)
    {
        this.emailService = emailService;
        this.repository = messageRepository;
        this.mapper = mapper;
    }
    [HttpPost]
    public async Task<IActionResult> SendEmail([FromBody] MessageRecipientsResource messageRecipients)
    {
        var message = await repository.GetMessage(messageRecipients.MessageId.Value);

        if (message == null)
            return BadRequest();

        var mimeMessage = mapper.Map<MessageRecipientsResource, MimeMessage>(messageRecipients);
        mapper.Map<Message, MimeMessage>(message, mimeMessage);

        var sendResult = await emailService.SendEmail(mimeMessage);

        if (!sendResult)
            return StatusCode(503, "Service Unavailable. Please try again later.");

        return Ok();
    }
}