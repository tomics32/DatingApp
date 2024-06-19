using DatingApp.Infrastructure.Data;
using DatingApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DatingApp.Application.interfaces;

namespace DatingApp.Api.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet] // /api/users
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return Ok(await _userRepository.GetUsersAsync());
           
        }

        [HttpGet("{username}")] // /api/users/lisa
        public async Task<ActionResult<AppUser>> GetUser(string username)
        {
            return await _userRepository.GetUserByNameAsync(username);
        }
    }
}
