using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.Application.DTOs;
using DatingApp.Application.Helpers;
using DatingApp.Application.Interfaces;
using DatingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace DatingApp.Infrastructure.Data;

public class MessageRepository(DataContext context, IMapper mapper) : IMessageRepository
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

    public async Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams)
    {
        var query = context.Messages.OrderByDescending(x => x.MessageSent).AsQueryable();

        query = messageParams.Container switch
        {
            "Inbox" => query.Where(x => x.Recipient.UserName == messageParams.Username),
            "Outbox" => query.Where(x => x.Sender.UserName == messageParams.Username),
            _ => query.Where(x => x.Recipient.UserName == messageParams.Username && x.DateRead == null)
        };

        var messages = query.ProjectTo<MessageDto>(mapper.ConfigurationProvider);

        return await CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
    }

    public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername)
    {
        var messages = await context.Messages
            .Include(x => x.Sender).ThenInclude(x => x.Photos)
            .Include(x => x.Recipient).ThenInclude(x => x.Photos).Where(x => x.RecipentUsername == currentUsername && x.SenderUsername == recipientUsername
            || x.SenderUsername == currentUsername && x.RecipentUsername == recipientUsername).OrderBy(x => x.MessageSent).ToListAsync();

        var unreadMessages = messages.Where(x => x.DateRead == null && x.RecipentUsername == currentUsername).ToList();

        if(unreadMessages.Count != 0)
        {
            unreadMessages.ForEach(x => x.DateRead = DateTime.UtcNow);
            await context.SaveChangesAsync();
        }

        return mapper.Map<IEnumerable<MessageDto>>(messages);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public static async Task<PagedList<T>> CreateAsync<T>(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}
