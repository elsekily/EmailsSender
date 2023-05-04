using AutoMapper;
using EmailSender.Core;
using EmailSender.Entities.Models;
using EmailSender.Entities.Resources;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMessageRepository repository;
    private readonly IMapper mapper;

    public MessageController(IUnitOfWork unitOfWork, IMessageRepository repository, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.repository = repository;
        this.mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMessage(int id)
    {
        var message = await repository.GetMessage(id);
        if (message == null)
            return NotFound();

        var result = mapper.Map<Message, MessageResource>(message);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetMessages()
    {
        var messages = await repository.GetMessages();

        var result = mapper.Map<IEnumerable<Message>, IEnumerable<MessageResource>>(messages);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMessage([FromBody] MessageSaveResource messageResource)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var message = mapper.Map<MessageSaveResource, Message>(messageResource);
        await repository.Add(message);

        await unitOfWork.CompleteAsync();

        message = await repository.GetMessage(message.Id);
        var result = mapper.Map<Message, MessageResource>(message);
        return Created(nameof(GetMessage), result);
    }
}