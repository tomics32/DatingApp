using System.Security.Claims;
using DatingApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace DatingApp.Infrastructure.Helpers;

public class LogUserActivity : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next();
        if (context.HttpContext.User.Identity?.IsAuthenticated != true)
        {
            return;
        }

        var userId = resultContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            throw new Exception("Cannot get ID of this user from token");
        }
        var userIdInt = int.Parse(userId);
        

        var repo = resultContext.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
        var user = await repo.GetUserByIdAsync(userIdInt);
        if (user == null)
        {
            return;
        }
        user.LastActive = DateTime.UtcNow;
        await repo.SaveAllAsync();
    }
}
