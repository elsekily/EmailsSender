
using EmailSender.Core;

namespace EmailSender.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly EmailSenderDbContext context;

    public UnitOfWork(EmailSenderDbContext context)
    {
        this.context = context;
    }
    public async Task CompleteAsync()
    {
        await context.SaveChangesAsync();
    }
}