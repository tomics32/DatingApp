using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using DatingApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Infrastructure.Data;

public class Seed
{
    public static async Task SeedUsers(UserManager<AppUser> userManager)
    {
        if(await userManager.Users.AnyAsync())
        {
            return;
        }

        var userData = await File.ReadAllTextAsync("../DatingApp.Infrastructure/Data/UserSeedData.json");

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var users = JsonSerializer.Deserialize<List<AppUser>>(userData, options);

        if (users == null)
        {
            return;
        }

        foreach (var user in users)
        {
            user.UserName = user.UserName!.ToLower();
            await userManager.CreateAsync(user, "Pa$$w0rd");
        }
    }
}
