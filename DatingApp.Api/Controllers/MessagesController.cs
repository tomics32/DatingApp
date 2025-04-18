using AutoMapper;
using DatingApp.Api.Extensions;
using DatingApp.Application.DTOs;
using DatingApp.Application.Interfaces;
using DatingApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Api.Controllers
{
    public class MessagesController(IMessageRepository messageRepository, IUserRepository userRepository, IMapper mapper) : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
        {
            var username = User.GetUsername();

            if (username == createMessageDto.RecipientUsername.ToLower())
            {
                return BadRequest("You cannot message yourself");
            }
            var sender = await userRepository.GetUserByUsernameAsync(username);
            var recipient = await userRepository.GetUserByUsernameAsync(createMessageDto.RecipientUsername);

            if (recipient == null || sender == null)
            {
                return BadRequest("Cannot send message at this time");
            }

            var message = new Message
            {
                Sender = sender,
                Recipient = recipient,
                SenderUsername = sender.UserName,
                RecipentUsername = recipient.UserName,
                Content = createMessageDto.Content
            };

            messageRepository.AddMessage(message);
            if (await messageRepository.SaveAllAsync())
            {
                return Ok(mapper.Map<MessageDto>(message));
            }

            return BadRequest("Failed to save message");
        }
    }
}
