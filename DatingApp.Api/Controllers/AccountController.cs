using System.Security.Cryptography;
using System.Text;
using DatingApp.Domain.Entities;
using DatingApp.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Api.Controllers;

public class AccountController(DataContext context) : BaseApiController
{
    [HttpPost("register")] // api/account/register
    public async Task<ActionResult<AppUser>> Register(string username, string password)
    {
        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            UserName = username,
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
            passwordSalt = hmac.Key
        };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        return user;
    }
}

