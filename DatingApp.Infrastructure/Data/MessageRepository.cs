using DatingApp.Application.DTOs;
using DatingApp.Application.Helpers;
using DatingApp.Application.Interfaces;
using DatingApp.Domain.Entities;

namespace DatingApp.Infrastructure.Data;

public class MessageRepository(DataContext context) : IMessageRepository
{
    public void AddMessage(Message message)
    {
        context.Messages.Add(message);
    }

    public void DeleteMessage(Message message)
    {
        context.Messages.Remove(message);
    }

    public async Task<Message?> GetMessage(int id)
    {
        return await context.Messages.FindAsync(id);
    }

    public Task<PagedList<MessageDto>> GetMessagesForUser()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
}
