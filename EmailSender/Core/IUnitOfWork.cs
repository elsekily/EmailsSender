namespace EmailSender.Core;

public interface IUnitOfWork
{
    Task CompleteAsync();
}