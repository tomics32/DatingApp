using DatingApp.Domain.Entities;

namespace DatingApp.Application.interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}