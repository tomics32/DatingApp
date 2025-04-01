using DatingApp.Application.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Infrastructure.Helpers;

public class PagedListService<T>
{
    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}
