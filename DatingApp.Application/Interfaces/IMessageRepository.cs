using DatingApp.Application.DTOs;
using DatingApp.Application.Helpers;
using DatingApp.Domain.Entities;


namespace DatingApp.Application.Interfaces;

public interface IMessageRepository
{
    void AddMessage(Message message);
    void DeleteMessage(Message message);

    Task<Message?> GetMessage(int id);
    Task<PagedList<MessageDto>> GetMessagesForUser();
    Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername);
    Task<bool> SaveAllAsync();
}
