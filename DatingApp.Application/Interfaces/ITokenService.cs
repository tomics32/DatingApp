using DatingApp.Domain.Entities;

namespace DatingApp.Application.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}


