using System.Security.Cryptography;
using System.Text;
using DatingApp.Application.DTOs;
using DatingApp.Application.Interfaces;
using DatingApp.Domain.Entities;
using DatingApp.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Api.Controllers;

public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
{
    [HttpPost("register")] // api/account/register
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.Username))
        {
            return BadRequest("Username is taken");
        }
        return Ok();
        /* using var hmac = new HMACSHA512();

         var user = new AppUser
         {
             UserName = registerDto.Username.ToLower(),
             passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
             passwordSalt = hmac.Key
         };
         context.Users.Add(user);
         await context.SaveChangesAsync();

         return new UserDto
         {
             Username = user.UserName,
             Token = tokenService.CreateToken(user)
        }; */
    }
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await context.Users.Include(p => p.Photos).FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

        if (user == null)
        {
            return Unauthorized("Username does not exist");
        }

        using var hmac = new HMACSHA512(user.passwordSalt);
        var computerHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < computerHash.Length; i++)
        {
            if (computerHash[i] != user.passwordHash[i])
            {
                return Unauthorized("Invalid Password");
            } 
        }
        return new UserDto
        {
            Username = user.UserName,
            Token = tokenService.CreateToken(user),
            PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain == true)?.Url
        };

    }
    private async Task<bool> UserExists(string username)
    {
        return await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());

    }
}

