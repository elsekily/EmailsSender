
using EmailSender.Core;
using EmailSender.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailSender.Persistence.Repositories;

public class MessagesRepository : IMessageRepository
{
    private readonly EmailSenderDbContext context;

    public MessagesRepository(EmailSenderDbContext context)
    {
        this.context = context;
    }

    public async Task Add(Message message)
    {
        await context.Messages.AddAsync(message);
    }

    public async Task<Message> GetMessage(int id)
    {
        return await context.Messages.Where(e => e.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Message>> GetMessages()
    {
        return await context.Messages.OrderByDescending(m => m.Id).ToListAsync();
    }

    public void Remove(Message message)
    {
        context.Messages.Remove(message);
    }
}