using EmailSender.Entities.Models;

namespace EmailSender.Core;

public interface IMessageRepository
{
    Task<IEnumerable<Message>> GetMessages();
    Task<Message> GetMessage(int id);
    Task Add(Message message);
    void Remove(Message message);
}